using System.IO;
using System.Threading.Tasks;
using Fuyu.Client.Common.Reflection;
using Fuyu.Client.EFT.Patches;
using Fuyu.Client.EFT.Utils;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using Microsoft.Win32;

namespace Fuyu.Client.EFT;

public class EFTMod : AbstractMod
{
    private readonly AbstractPatch[] _patches;

    public override string Id { get; } = "com.Fuyu.Client.eft";

    public override string Name { get; } = "Fuyu.Client.EFT";

    public EFTMod()
    {
        _patches = new AbstractPatch[]
        {
            new BattlEyePatch(),
            new ConsistencyGeneralPatch()
        };
    }

    public override Task OnLoad(DependencyContainer container)
    {
        Terminal.WriteLine("Validating...");
        ValidateGameCopy();

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
        var registryPath = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\EscapeFromTarkov";
        var registryEntry = Registry.LocalMachine.OpenSubKey(registryPath, false).GetValue("InstallLocation");
        var installationPath = string.Empty;
        
        if (registryEntry != null)
        {
            installationPath = registryEntry.ToString();
        }
        else
        {
            throw new Exception("Could not find Live EFT installation directory. Please ensure you ran Live EFT at least once on your machine.");
            Terminal.WriteLine(ex);
            throw ex;
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
                var ex = new Exception("The Live EFT installation either does not exist or is damaged. Please validate your game files in BsgLauncher");
                Terminal.WriteLine(ex);
                throw ex;
            }
        }
    }
}
