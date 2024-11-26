// Disable general file validation, does NOT disable client bundles validation

using System.Reflection;
using System.Threading.Tasks;
using EFT;
using Fuyu.Plugin.Common.Reflection;
using Fuyu.Plugin.EFT.Reflection;

namespace Fuyu.Plugin.EFT.Patches
{
    public class ConsistencyGeneralPatch : APatch
    {
        private static readonly MethodInfo _mi;

        static ConsistencyGeneralPatch()
        {
            var flags = PatchHelper.PrivateFlags;
            _mi = typeof(TarkovApplication).BaseType.GetMethod("RunFilesChecking", flags);
        }

        public ConsistencyGeneralPatch() : base("com.fuyu.plugin.eft.consistencygeneral", EPatchType.Prefix)
        {
        }

        protected override MethodBase GetOriginalMethod()
        {
            return _mi;
        }

        protected static bool Patch(ref Task __result)
        {
            __result = Task.CompletedTask;

            // Don't run original code
            return false;
        }
    }
}