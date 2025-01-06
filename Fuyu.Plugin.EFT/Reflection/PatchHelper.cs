using System;

namespace Fuyu.Plugin.EFT.Reflection
{
    public static class PatchHelper
    {
        public static readonly Type[] Types;

        static PatchHelper()
        {
            Types = typeof(ETransportProtocolType).Assembly.GetTypes();
        }
    }
}