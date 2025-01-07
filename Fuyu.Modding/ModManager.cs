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
		public static ModManager Instance => instance.Value;
		private static readonly Lazy<ModManager> instance = new Lazy<ModManager>(() => new ModManager());

		/// <summary>
		/// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
		/// </summary>
		private ModManager()
		{

		}

        private readonly List<Mod> _mods = new List<Mod>();

        private string GetResourcePath(string resourceRootPath, string resourcePath)
        {
            return resourcePath
                .Replace(resourceRootPath + "\\", string.Empty) // exp. Fuyu/Mods/Launcher
                .Replace("../", string.Empty)                   // relative path ./
                .Replace("./", string.Empty)                    // relative path ./
                .Replace("\\", ".")                             // windows \
                .Replace("/", ".");                             // unix /
        }

        private CSharpCompilation CreateCompilation(
            string assemblyName,
            IEnumerable<SyntaxTree> syntaxTrees,
            bool includeReferences)
        {
            // TODO: Simplify this later
            IEnumerable<PortableExecutableReference> references;

            if (includeReferences)
            {
                references =
                    // Mods can reference everything we have loaded
                    AppDomain.CurrentDomain.GetAssemblies()
                    // Where it is a disk on file
                    .Where(a => !string.IsNullOrEmpty(a.Location))
                    // Create a MetadataReference from it
                    .Select(a => MetadataReference.CreateFromFile(a.Location));
            }
            else
            {
                references = Array.Empty<PortableExecutableReference>();
            }

            return CSharpCompilation.Create(
                assemblyName,
                syntaxTrees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );
        }

        public void AddMods(string directory)
        {
            if (!VFS.DirectoryExists(directory))
            {
                VFS.CreateDirectory(directory);
                return;
            }

            var subdirectories = VFS.GetDirectories(directory);

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
            if (VFS.GetFiles(directory, "*.dll").Length > 0)
            {
                return EModType.DLL;
            }

            if (VFS.DirectoryExists(Path.Combine(directory, "src")))
            {
                if (VFS.GetFiles(Path.Combine(directory, "src"), "*.cs").Length > 0)
                {
                    return EModType.Source;
                }
            }

            return EModType.Invalid;
        }

        private void ProcessDLLMod(string directory)
        {
            var dllPaths = VFS.GetFiles(directory, "*.dll");

            foreach (var dllPath in dllPaths)
            {
                var assembly = Assembly.LoadFrom(dllPath);
                ProcessAssembly(assembly, EModType.DLL);
            }
        }

        private void ProcessSourceFiles(string directory)
        {
            var sourceFiles = VFS.GetFiles(Path.Combine(directory, "src"), "*.cs", SearchOption.AllDirectories);

            if (sourceFiles.Length == 0)
            {
                throw new Exception($"{directory} was marked as a source mod yet no source files were found");
            }

            var assemblyName = Path.GetFileName(directory);
            var syntaxTrees = new List<SyntaxTree>(sourceFiles.Length);

            foreach (var file in sourceFiles)
            {
                var fileContents = VFS.ReadTextFile(file);
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

            var resourceRootPath = Path.Combine(directory, "res");
            var resourcePaths = VFS.GetFiles(resourceRootPath, "*.*", SearchOption.AllDirectories);
            var resources = resourcePaths.Select(resourcePath =>
            {
                var fileName = $"{assemblyName}.Resources.{GetResourcePath(resourceRootPath, resourcePath)}";

                return new ResourceDescription(
                    fileName,
                    () => VFS.OpenRead(resourcePath),
                    isPublic: true
                );
            }).ToArray();

            var compilation = CreateCompilation(assemblyName, syntaxTrees, true);

            Assembly assembly;

            // Intentionally left open so that we can use it later
            using (var ms = new MemoryStream())
            {
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
                assembly = Assembly.Load(ms.ToArray());
            }

            Resx.SetSource(assemblyName, assembly);
            ProcessAssembly(assembly, EModType.Source);
        }

        private void ProcessAssembly(Assembly assembly, EModType assemblyModType)
        {
            if (assemblyModType == EModType.DLL)
            {
                var resourceAssembly = GenerateResourceAssembly(assembly);
                if (resourceAssembly != null)
                {
                    Resx.SetSource(assembly.GetName().Name, resourceAssembly);
                }
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
            for (var i = 0; i < _mods.Count; i++)
            {
                var mod = _mods[i];

                if (mod.Dependencies != null)
                {
                    foreach (var dependency in mod.Dependencies)
                    {
                        if (_mods.FindIndex(m => m.Id == dependency) == -1)
                        {
                            throw new Exception($"{mod.Id} is missing dependency {dependency} and will not be loaded");
                        }
                    }
                }
            }
        }

        private void SortMods()
        {
            // TODO: readd later, based on dependency tree
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

        private Assembly GenerateResourceAssembly(Assembly sourceAssembly)
        {
            var assemblyName = sourceAssembly.GetName().Name;
            var resourceRootPath = Path.Combine(Path.GetDirectoryName(sourceAssembly.Location), "res");
            string[] resourcePaths;

            try
            {
                resourcePaths = VFS.GetFiles(resourceRootPath, "*.*", SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }

            if (resourcePaths.Length == 0)
            {
                return null;
            }

            var resources = resourcePaths.Select(resourcePath =>
            {
                var fileName = $"{assemblyName}.Resources.{GetResourcePath(resourceRootPath, resourcePath)}";

                return new ResourceDescription(
                    fileName,
                    () => VFS.OpenRead(resourcePath),
                    isPublic: true
                );
            }).ToArray();

            var compilation = CreateCompilation($"{assemblyName}.Resources", Array.Empty<SyntaxTree>(), false);

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

                return resourceAssembly;
            }
        }
    }
}