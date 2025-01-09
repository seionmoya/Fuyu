using System;
using System.Collections.Generic;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using NLog;
using NLog.Targets;

[Target(nameof(FuyuClient))]
public sealed class FuyuClient : TargetWithLayout
{
    protected override void InitializeTarget()
    {
        var container = new DependencyContainer();

        CheckIncompatibleSoftware();

        Terminal.WriteLine("Loading mods...");
        ModManager.Instance.AddMods("./Fuyu/Mods/Client");
        ModManager.Instance.Load(container).GetAwaiter().GetResult();
        Terminal.WriteLine("Finished loading mods");

        // TODO: OnApplicationQuit
        // -- seionmoya, 20205-01-04
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
            if (VFS.Exists(kvp.Key))
            {
                throw new Exception($"{kvp.Value} found. Please remove the software from the client before proceeding.");
            }
        }
    }
}