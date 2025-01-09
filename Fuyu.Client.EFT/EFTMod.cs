using System.Threading.Tasks;
using Fuyu.Client.Common.Reflection;
using Fuyu.Client.EFT.Patches;
using Fuyu.Client.EFT.Utils;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;

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
}