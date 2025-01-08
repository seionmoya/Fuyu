using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using Fuyu.Plugin.Arena.Patches;
using Fuyu.Plugin.Arena.Utils;
using Fuyu.Plugin.Common.Reflection;

namespace Fuyu.Plugin.Arena
{
    public class ArenaMod : AbstractMod
    {
        private readonly AbstractPatch[] _patches;

        public override string Id { get; } = "com.fuyu.plugin.arena";

        public override string Name { get; } = "Fuyu.Plugin.Arena";

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
    }
}