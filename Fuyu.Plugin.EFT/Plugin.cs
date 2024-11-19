using BepInEx;
using Fuyu.Plugin.Common.Utils;
using Fuyu.Plugin.Common.Reflection;
using Fuyu.Plugin.EFT.Patches;
using Fuyu.Plugin.EFT.Utils;

namespace Fuyu.Plugin.EFT
{
    [BepInPlugin("com.fuyu.plugin.eft", "Fuyu.Plugin.EFT", "1.0.0")]
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