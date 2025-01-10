using System.Collections.Generic;
using System.Linq;
using Fuyu.Client.Arena.Reflection;
using Fuyu.Common.IO;
using HarmonyLib;

namespace Fuyu.Client.Arena.Utils;

public static class ProtocolUtil
{
    // NOTE: A dirty hack, probably needs to be implemented cleanly later.
    //       Since BackendName already contains the protocol, just never
    //       use the entries.
    // -- seionmoya, 2024/08/xx 
    public static void RemoveTransportPrefixes()
    {
        Terminal.WriteLine("Removing transport prefixes...");

        var target = "TransportPrefixes";
        var type = PatchHelper.Types.Single(t => t.GetField(target) != null);
        var value = Traverse.Create(type).Field(target).GetValue<Dictionary<ETransportProtocolType, string>>();

        value[ETransportProtocolType.HTTPS] = string.Empty;
        value[ETransportProtocolType.WSS] = string.Empty;
    }
}