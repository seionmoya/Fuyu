using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Accounts
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ESessionMode
    {
        [EnumMember(Value = "regular")]
        Regular,
        [EnumMember(Value = "pve")]
        Pve
    }
}
