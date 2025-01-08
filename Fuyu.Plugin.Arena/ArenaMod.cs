using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using Fuyu.Plugin.Arena.Patches;
using Fuyu.Plugin.Arena.Utils;
using Fuyu.Plugin.Common.Reflection;
using Fuyu.Plugin.Common.Utils;

namespace Fuyu.Plugin.Arena
{
    public class ArenaMod : Mod
    {
        private readonly APatch[] _patches;

        public override string Id { get; } = "com.fuyu.plugin.arena";

        public override string Name { get; } = "Fuyu.Plugin.Arena";

        public ArenaMod()
        {
            _patches = new APatch[]
            {
                new BattlEyePatch(),
                new ConsistencyGeneralPatch()
            };
        }

        public override Task OnLoad(DependencyContainer container)
        {
            LogWriter.Initialize(Logger, GetType().Assembly);

            LogWriter.WriteLine("Patching...");

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
            LogWriter.WriteLine("Unpatching...");

            foreach (var patch in _patches)
            {
                patch.Disable();
            }

            return Task.CompletedTask;
        }
    }
}