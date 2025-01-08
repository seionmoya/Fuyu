using System;
using System.Reflection;
using Fuyu.Common.IO;
using HarmonyLib;

namespace Fuyu.Client.Common.Reflection
{
    public abstract class AbstractPatch
    {
        public readonly string Id;
        public readonly EPatchType Type;
        public readonly Harmony Harmony;

        public AbstractPatch(string id, EPatchType type)
        {
            Id = id;
            Type = type;
            Harmony = new Harmony(Id);
        }

        protected abstract MethodBase GetOriginalMethod();

        private HarmonyMethod GetPatchMethod()
        {
            var mi = GetType().GetMethod("Patch", BindingFlags.NonPublic | BindingFlags.Static);
            return new HarmonyMethod(mi);
        }

        public void Enable()
        {
            Terminal.WriteLine($"Enabling: {Id}");

            var patch = GetPatchMethod();
            var target = GetOriginalMethod();

            if (target == null)
            {
                throw new InvalidOperationException($"{Id}: GetOriginalMethod returns null");
            }

            switch (Type)
            {
                case EPatchType.Prefix:
                    Harmony.Patch(target, prefix: patch);
                    return;

                case EPatchType.Postfix:
                    Harmony.Patch(target, postfix: patch);
                    return;

                case EPatchType.Transpile:
                    Harmony.Patch(target, transpiler: patch);
                    return;

                default:
                    throw new NotImplementedException("Patch type");
            }
        }

        public void Disable()
        {
            Terminal.WriteLine($"Disabling: {Id}");

            Harmony.UnpatchSelf();
        }
    }
}