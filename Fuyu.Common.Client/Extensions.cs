using HarmonyLib;

namespace Fuyu.Common.Client;

public static class Extensions
{
    // CodeQL reports error on not disposing harmony.
    // Harmony got no Dispose implemented, this fixes it.
    public static void Dispose(this Harmony harmony)
    {
        harmony.UnpatchSelf();
    }
}