// Prevent BE from detecting injected assemblies.

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fuyu.Client.Common.Reflection;
using Fuyu.Client.EFT.Reflection;

namespace Fuyu.Client.EFT.Patches;

public class BattlEyePatch : AbstractPatch
{
    private static readonly PropertyInfo _succeed;
    private static readonly MethodInfo _mi;

    static BattlEyePatch()
    {
        var name = "RunValidation";
        var type = PatchHelper.Types.Single(t => t.GetMethod(name) != null);

        _succeed = type.GetProperties().Single(p => p.Name == "Succeed");
        _mi = type.GetMethod(name);
    }

    public BattlEyePatch() : base("Fuyu.Client.EFT.BattlEyePatch", EPatchType.Prefix)
    {
    }

    protected override MethodBase GetOriginalMethod()
    {
        return _mi;
    }

    protected static bool Patch(ref Task __result, object __instance)
    {
        _succeed.SetValue(__instance, true);
        __result = Task.CompletedTask;

        // Don't run original code
        return false;
    }
}