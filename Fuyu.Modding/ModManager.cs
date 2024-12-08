using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Fuyu.Modding
{
    // This should live during the entire lifetime of the application
    public class ModManager
    {
        private static ModManager _instance;

        private readonly List<Mod> _mods = new List<Mod>();

        private readonly List<Assembly> _moddedAssemblies = new List<Assembly>();

        private readonly List<MemoryStream> _dynamicMods = new List<MemoryStream>();

        private static readonly object _lock = new object();

        public static ModManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ModManager();
                    }

                    return _instance;
                }
            }
        }

        public ModManager()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Find can return null and that's a valid result and I
            // feel like it's cleaner then explicitly returning null
            // -- nexus4880, 2024-12-7
            return _moddedAssemblies.Find(a => a.FullName == args.Name);
        }

		private CSharpCompilation CreateCompilation(
            string assemblyName,
            IEnumerable<SyntaxTree> syntaxTrees,
            bool includeReferences = true)
        {
            // TODO: Simplify this later
            IEnumerable<PortableExecutableReference> references =
                includeReferences ?
					// Mods can reference everything we have loaded
					AppDomain.CurrentDomain.GetAssemblies()
                    // Where it is a disk on file
                    .Where(a => !string.IsNullOrEmpty(a.Location))
                    // Create a MetadataReference from it
                    .Select(a => MetadataReference.CreateFromFile(a.Location))
                    // AND grab from our already loaded dynamic mods as well
                    .Concat(_dynamicMods.Select(s => MetadataReference.CreateFromStream(s))) :
                Array.Empty<PortableExecutableReference>();

			return CSharpCompilation.Create(
				assemblyName,
				syntaxTrees,
				references,
				new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
			);
        }

		public void AddMods(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return;
            }

            var subdirectories = Directory.GetDirectories(directory);

            foreach (var modDirectory in subdirectories)
            {
                var modType = GetModType(modDirectory);
                
                switch (modType)
                {
                    case EModType.DLL:
                        ProcessDLLMod(modDirectory);
                        break;
                    case EModType.Source:
                        ProcessSourceFiles(modDirectory);
                        break;
                    case EModType.Invalid:
						throw new Exception($"{modDirectory} does not contain a valid mod setup");
				}
            }
        }

        private EModType GetModType(string directory)
        {
            if (Directory.GetFiles(directory, "*.dll").Length > 0)
            {
                return EModType.DLL;
            }

            if (Directory.Exists(Path.Combine(directory, "src")))
			{
                if (Directory.GetFiles(Path.Combine(directory, "src"), "*.cs").Length > 0)
                {
				    return EModType.Source;
                }
			}

            return EModType.Invalid;
        }

        private void ProcessDLLMod(string directory)
        {
            var dllPaths = Directory.GetFiles(directory, "*.dll");

            foreach (var dllPath in dllPaths)
            {
                var assembly = Assembly.LoadFrom(dllPath);
                ProcessAssembly(assembly, EModType.DLL);
			}
        }

        private void ProcessSourceFiles(string directory)
        {
            var sourceFiles = Directory.GetFiles(Path.Combine(directory, "src"), "*.cs", SearchOption.AllDirectories);

			if (sourceFiles.Length == 0)
            {
                throw new Exception($"{directory} was marked as a source mod yet no source files were found");
            }

            var assemblyName = Path.GetFileName(directory);
			var syntaxTrees = new List<SyntaxTree>(sourceFiles.Length);

			foreach (var file in sourceFiles)
			{
				var fileContents = File.ReadAllText(file);
				var syntaxTree = CSharpSyntaxTree.ParseText(
                    fileContents,
                    new CSharpParseOptions(
                        LanguageVersion.Latest,
                        DocumentationMode.None,
                        SourceCodeKind.Regular
                    ),
                    file
                );

				syntaxTrees.Add(syntaxTree);
			}

			var resourcePaths = Directory.GetFiles(Path.Combine(directory, "res"), "*.*", SearchOption.AllDirectories);
			var resources = resourcePaths.Select(resourcePath =>
			{
				var fileName = $"{assemblyName}.embedded.{Path.GetFileName(resourcePath)}";

				return new ResourceDescription(
					fileName,
					() => File.OpenRead(resourcePath),
					isPublic: true
				);
			}).ToArray();

			var compilation = CreateCompilation(assemblyName, syntaxTrees);

			// Intentionally left open so that we can use it later
			var ms = new MemoryStream();
			_dynamicMods.Add(ms);

            var emitResult = compilation.Emit(ms, manifestResources: resources);
            if (!emitResult.Success)
            {
				var errors = emitResult.Diagnostics
	                .Where(d => !d.IsSuppressed && d.Severity == DiagnosticSeverity.Error);

				// Technically no cleanup is being done here but that's because this is considered a
				// failed state and the ModManager should no longer live (and therefore the program)
				// -- nexus4880, 2024-12-3

				throw new Exception(string.Join(Environment.NewLine, errors));
			}

			ms.Position = 0;
			var assembly = Assembly.Load(ms.ToArray());
			Resx.SetSource(assemblyName, assembly);
			ProcessAssembly(assembly, EModType.Source);
		}

		private void ProcessAssembly(Assembly assembly, EModType assemblyModType)
        {
            if (assemblyModType == EModType.DLL)
			{
				var assemblyName = assembly.GetName().Name;
                string[] resourcePaths;

                try
                {
					resourcePaths = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(assembly.Location), "res"), "*.*", SearchOption.AllDirectories);
				}
                catch (DirectoryNotFoundException)
                {
                    resourcePaths = Array.Empty<string>();
                }

                if (resourcePaths.Length > 0)
                {
					var resources = resourcePaths.Select(resourcePath =>
					{
						var fileName = $"{assemblyName}.embedded.{Path.GetFileName(resourcePath)}";

						return new ResourceDescription(
							fileName,
							() => File.OpenRead(resourcePath),
							isPublic: true
						);
					}).ToArray();

					var compilation = CreateCompilation($"{assemblyName}.Resources", Array.Empty<SyntaxTree>());

                    using (var assemblyStream = new MemoryStream())
                    {
						var emitResult = compilation.Emit(assemblyStream, manifestResources: resources);

						if (!emitResult.Success)
						{
							var errors = emitResult.Diagnostics
								.Where(d => !d.IsSuppressed && d.Severity == DiagnosticSeverity.Error);

							// Technically no cleanup is being done here but that's because this is considered a
							// failed state and the ModManager should no longer live (and therefore the program)
							// -- nexus4880, 2024-12-3

							throw new Exception(string.Join(Environment.NewLine, errors));
						}

                        var resourceAssembly = Assembly.Load(assemblyStream.ToArray());
						Resx.SetSource(assembly.GetName().Name, resourceAssembly);
					}
				}
			}

			if (!_moddedAssemblies.Contains(assembly))
            {
                _moddedAssemblies.Add(assembly);
			}

			// Get types where T inherits from Mod
			var modTypes = assembly.GetExportedTypes()
				.Where(t => typeof(Mod).IsAssignableFrom(t));

			// Technically you could export multiple mods per assembly
			foreach (var modType in modTypes)
			{
				var mod = (Mod)Activator.CreateInstance(modType);

                if (_mods.FindIndex(m => m.Id == mod.Id) != -1)
                {
                    throw new Exception($"A mod with the id {mod.Id} has already been added");
                }

				Terminal.WriteLine($"Adding mod {mod.Name} ({mod.Id})");
				_mods.Add(mod);
			}
		}

        public async Task Load(DependencyContainer container)
		{
			CheckDependencies();
			SortMods();

			foreach (var mod in _mods)
			{
				// All mods WILL be registered in the DependencyContainer
				container.RegisterSingleton(mod.Id, mod);
			}

            foreach (var mod in _mods)
            {
                if (!mod.IsLoaded)
                { 
                    await mod.OnLoad(container);
                    mod.IsLoaded = true;
				}
            }
        }

        private void CheckDependencies()
        {
			var runAgain = false;

			for (var i = 0; i < _mods.Count; i++)
			{
				var mod = _mods[i];
				var hasAllDependencies = true;

				if (mod.Dependencies != null)
				{
					foreach (var dependency in mod.Dependencies)
					{
						if (_mods.FindIndex(m => m.Id == dependency) == -1)
						{
							hasAllDependencies = false;
							Terminal.WriteLine($"{mod.Id} is missing dependency {dependency} and will not be loaded");
						}
					}
				}

				if (!hasAllDependencies)
				{
					_mods.RemoveAt(i);
					i--;
					runAgain = true;
				}
			}

			if (runAgain)
			{
				CheckDependencies();
			}
		}

        private void SortMods()
        {

        }

		public async Task UnloadAll()
		{
            while (_mods.Count > 0)
            {
                await UnloadMod(_mods[_mods.Count - 1]);
            }
		}

        private async Task UnloadMod(Mod mod)
        {
            if (mod.IsLoaded)
            {
                Terminal.WriteLine($"[{mod.Name} - ({mod.Id})] Unloading");
                await mod.OnShutdown();
                mod.IsLoaded = false;
            }

            _mods.Remove(mod);
        }
	}
}