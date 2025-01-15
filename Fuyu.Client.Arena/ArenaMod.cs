using System;
using System.IO;
using System.Threading.Tasks;
using Fuyu.Client.Arena.Patches;
using Fuyu.Client.Arena.Utils;
using Fuyu.Client.Common.Reflection;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using Microsoft.Win32;

namespace Fuyu.Client.Arena;

public class ArenaMod : AbstractMod
{
    private readonly AbstractPatch[] _patches;

    public override string Id { get; } = "com.Fuyu.Client.arena";

    public override string Name { get; } = "Fuyu.Client.Arena";

    public ArenaMod()
    {
        _patches = new AbstractPatch[]
        {
            new BattlEyePatch(),
            new ConsistencyGeneralPatch()
        };
    }

    public override Task OnLoad(DependencyContainer container)
    {
        Terminal.WriteLine("Patching...");

        // TODO: disable when running on HTTPS
        // -- seionmoya, 2024-11-19
        ProtocolUtil.RemoveTransportPrefixes();

        foreach (var patch in _patches)
        {
            patch.Enable();
        }

        return Task.CompletedTask;
    }

    public override Task OnShutdown()
    {
        Terminal.WriteLine("Unpatching...");

        foreach (var patch in _patches)
        {
            patch.Disable();
        }

        return Task.CompletedTask;
    }

    public void ValidateGameCopy()
    {
        var registryPath = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\EscapeFromTarkovArena";
        var registryEntry = Registry.LocalMachine.OpenSubKey(registryPath, false).GetValue("InstallLocation");
        var installationPath = string.Empty;

        if (registryEntry != null)
        {
            installationPath = registryEntry.ToString();
        }
        else
        {
            throw new Exception("Could not find Live EFT Arena installation directory. Please ensure you ran Live EFT Arena at least once on your machine.");
        }

        var paths = new FileSystemInfo[]
        {
            new DirectoryInfo(installationPath),
            new FileInfo(Path.Combine(installationPath, "BattlEye/BEClient_x64.dll")),
            new FileInfo(Path.Combine(installationPath, "BattlEye/BEService_x64.exe")),
            new FileInfo(Path.Combine(installationPath, "ConsistencyInfo")),
            new FileInfo(Path.Combine(installationPath, "EscapeFromTarkov_BE.exe")),
            new FileInfo(Path.Combine(installationPath, "Uninstall.exe")),
            new FileInfo(Path.Combine(installationPath, "UnityCrashHandler64.exe"))
        };

        foreach (var info in paths)
        {
            if (!File.Exists(info.FullName))
            {
                throw new Exception("The Live EFT Arena installation either does not exist or is damaged. Please validate the integrity of your installation in BsgLauncher");
            }
        }
    }
}