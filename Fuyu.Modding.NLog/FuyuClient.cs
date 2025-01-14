using System;
using System.IO;
using System.Collections.Generic;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using NLog.Targets;

[Target(nameof(FuyuClient))]
public sealed class FuyuClient : TargetWithLayout
{
    // TODO: Client logging support
    // -- seionmoya, 2024-01-14
    protected override void InitializeTarget()
    {
        var container = new DependencyContainer();

        // Terminal.SetLogFile("Fuyu/Logs/Client.log");

        CheckIncompatibleSoftware();

        // Terminal.WriteLine("Loading mods...");
        ModManager.Instance.AddMods("./Fuyu/Mods/Client");
        ModManager.Instance.Load(container).GetAwaiter().GetResult();
        // Terminal.WriteLine("Finished loading mods");
    }

    private void CheckIncompatibleSoftware()
    {
        var record = new Dictionary<string, string>()
        {
            { "BepInEx/", "BepInEx" },
            { "MelonLoader/", "MelonLoader" },
            { "SPT.Launcher/", "SPTarkov" },
            { "monomod.exe", "MonoMod" },
            { "MonoMod.RuntimeDetour.HookGen.exe", "MonoMod" }
            // TODO: UnityModManager, UMF
        };

        foreach (var kvp in record)
        {
            if (File.Exists(kvp.Key))
            {
                throw new Exception($"{kvp.Value} found. Please remove the software from the client before proceeding.");
            }
        }
    }
}