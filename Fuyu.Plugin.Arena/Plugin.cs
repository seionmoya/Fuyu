using BepInEx;
using Fuyu.Plugin.Arena.Patches;
using Fuyu.Plugin.Arena.Utils;
using Fuyu.Plugin.Common.Reflection;
using Fuyu.Plugin.Common.Utils;

namespace Fuyu.Plugin.Arena
{
    [BepInPlugin("com.fuyu.plugin.arena", "Fuyu.Plugin.Arena", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly APatch[] _patches;

        public Plugin()
        {
            _patches = new APatch[]
            {
                new BattlEyePatch(),
                new ConsistencyGeneralPatch()
            };
        }

        protected void Awake()
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
        }

        protected void OnApplicationQuit()
        {
            LogWriter.WriteLine("Unpatching...");

            foreach (var patch in _patches)
            {
                patch.Disable();
            }
        }
    }
}