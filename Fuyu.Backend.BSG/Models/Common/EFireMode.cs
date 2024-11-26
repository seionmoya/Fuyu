using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.DTO.Common
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EFireMode : byte
    {
        [EnumMember(Value = "fullauto")]
        FullAuto = 0,
        [EnumMember(Value = "single")]
        Single = 1,
        [EnumMember(Value = "doublet")]
        Doublet = 2,
        [EnumMember(Value = "burst")]
        Burst = 3,
        [EnumMember(Value = "doubleaction")]
        DoubleAction = 4,
        [EnumMember(Value = "semiauto")]
        SemiAuto = 5,
        [EnumMember(Value = "grenadeThrowing")]
        GrenadeThrowing = 6,
        [EnumMember(Value = "greanadePlanting")]
        GrenadePlanting = 7
    }
}
