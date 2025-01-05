// Disable general file validation, does NOT disable client bundles validation

using System.Reflection;
using System.Threading.Tasks;
using EFT;
using Fuyu.Plugin.Common.Reflection;

namespace Fuyu.Plugin.EFT.Patches
{
    public class ConsistencyGeneralPatch : APatch
    {
        public ConsistencyGeneralPatch() : base("com.fuyu.plugin.eft.consistencygeneral", EPatchType.Prefix)
        {
        }

        protected override MethodBase GetOriginalMethod()
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            return typeof(TarkovApplication).BaseType.GetMethod("RunFilesChecking", flags);
        }

        protected static bool Patch(ref Task __result)
        {
            __result = Task.CompletedTask;

            // Don't run original code
            return false;
        }
    }
}