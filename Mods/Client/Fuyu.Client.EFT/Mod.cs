using System.Threading.Tasks;
using Fuyu.Common.Client.Reflection;
using Fuyu.Client.EFT.Patches;
using Fuyu.Client.EFT.Utils;
using Fuyu.DependencyInjection;
using Fuyu.Modding;

namespace Fuyu.Client.EFT;

public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Client.EFT";
    public override string Name { get; } = "Fuyu.Client.EFT";

    private readonly AbstractPatch[] _patches;

    public Mod()
    {
        _patches = [
            new BattlEyePatch(),
            new ConsistencyGeneralPatch()
        ];
    }

    public override Task OnLoad(DependencyContainer container)
    {
        // TODO: disable when running on HTTPS
        // -- seionmoya, 2024-11-19
        new ProtocolUtil().RemoveTransportPrefixes();

        foreach (var patch in _patches)
        {
            patch.Enable();
        }

        return Task.CompletedTask;
    }

    public override Task OnShutdown()
    {
        foreach (var patch in _patches)
        {
            patch.Disable();
        }

        return Task.CompletedTask;
    }
}