using System.Threading.Tasks;
using Fuyu.Client.Arena.Patches;
using Fuyu.Client.Arena.Utils;
using Fuyu.Common.Client.Reflection;
using Fuyu.DependencyInjection;
using Fuyu.Modding;

namespace Fuyu.Client.Arena;

public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Client.Arena";
    public override string Name { get; } = "Fuyu.Client.Arena";

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