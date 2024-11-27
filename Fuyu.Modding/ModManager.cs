using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;

namespace Fuyu.Modding
{
	// This should live during the entire lifetime of the application
	public class ModManager
	{
		private static ModManager _instance;

		private readonly List<Mod> _sortedMods = new List<Mod>();

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
			foreach (var mod in _sortedMods)
			{
				var modAssembly = mod.GetType().Assembly;
				if (modAssembly.FullName == args.Name)
				{
					return modAssembly;
				}
			}

			return null;
		}

		public Task Load(DependencyContainer container)
		{
			var modsDirectory = Path.Combine(Environment.CurrentDirectory, "Fuyu", "Mods");
			if (!Directory.Exists(modsDirectory))
			{
				Directory.CreateDirectory(modsDirectory);
				return Task.CompletedTask;
			}

			var dllFiles = Directory.GetFiles(modsDirectory, "*.dll");
			if (dllFiles.Length == 0)
			{
				return Task.CompletedTask;
			}

			return Load_Internal(dllFiles, container);
		}

		private async Task Load_Internal(string[] filePaths, DependencyContainer container)
		{
			List<Mod> mods = new List<Mod>();
			foreach (var filePath in filePaths)
			{
				// Load the dll
				var bytes = File.ReadAllBytes(filePath);
				var assembly = AppDomain.CurrentDomain.Load(bytes);

				// Get types where T implements IMod
				var modTypes = assembly.GetExportedTypes()
					.Where(t => typeof(Mod).IsAssignableFrom(t));

				// Technically you could export multiple mods per file
				foreach (var modType in modTypes)
				{
					var mod = (Mod)Activator.CreateInstance(modType);

					Terminal.WriteLine($"Adding mod {mod.Name}");

					mods.Add(mod);
				}
			}

			if (mods.Count == 0)
			{
				return;
			}

			CheckDependencies(mods);
			SortMods(mods);

			Terminal.WriteLine($"Current order: {string.Join(", ", _sortedMods.Select(p => p.Name))}");

			foreach (var mod in _sortedMods)
			{
				// All mods will be registered to the container
				container.RegisterSingleton(mod.Id, mod);
			}

			foreach (var mod in _sortedMods)
			{
				Terminal.WriteLine($"Loading mod {mod.Name}");
				await mod.OnLoad(container);
				mod.IsLoaded = true;
			}
		}

		private void SortMods(List<Mod> mods)
		{
			foreach (var mod in mods)
			{
				AddModToSorted(mods, mod, null);
			}
		}

		private void AddModToSorted(List<Mod> mods, Mod mod, Mod dependent)
		{
			if (_sortedMods.Contains(mod))
			{
				return;
			}

			if (mod.Dependencies != null)
			{
				foreach (var dependency in mod.Dependencies)
				{
					if (dependent != null && dependent.Id == dependency)
					{
						throw new Exception($"Cyclic dependency {mod.Id} <-> {dependency}");
					}

					AddModToSorted(mods, mods.Find(m => m.Id == dependency), mod);
				}
			}

			_sortedMods.Add(mod);
		}

		private void CheckDependencies(List<Mod> mods)
		{
			var runAgain = false;

			for (var i = 0; i < mods.Count; i++)
			{
				var mod = mods[i];
				var hasAllDependencies = true;

				if (mod.Dependencies != null)
				{
					foreach (var dependency in mod.Dependencies)
					{
						if (mods.FindIndex(m => m.Id == dependency) == -1)
						{
							hasAllDependencies = false;
							Terminal.WriteLine($"{mod.Id} is missing dependency {dependency} and will not be loaded");
						}
					}
				}

				if (!hasAllDependencies)
				{
					mods.RemoveAt(i);
					i--;
					runAgain = true;
				}
			}

			if (runAgain)
			{
				CheckDependencies(mods);
			}
		}

		private async Task Unload(Mod mod)
		{
			if (mod.IsLoaded)
			{
				Terminal.WriteLine($"Unloading mod {mod.Name}");
				await mod.OnShutdown();
				mod.IsLoaded = false;
			}

			_sortedMods.Remove(mod);
		}

		public async Task UnloadAll()
		{
			while (_sortedMods.Count > 0)
			{
				await Unload(_sortedMods[_sortedMods.Count - 1]);
			}
		}
	}
}
