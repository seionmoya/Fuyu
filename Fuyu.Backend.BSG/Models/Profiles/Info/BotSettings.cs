using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Profiles.Info;

[DataContract]
public class BotSettings
{
    [DataMember]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public EBotRole Role { get; set; }

    [DataMember]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public EBotDifficulty BotDifficulty { get; set; }

    [DataMember]
    public int Experience { get; set; }

    [DataMember]
    public float StandingForKill { get; set; }

    [DataMember]
    public float AggressorBonus { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public bool? UseSimpleAnimator { get; set; }
}