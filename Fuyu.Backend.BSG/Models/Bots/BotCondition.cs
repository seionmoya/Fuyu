using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Bots;

[DataContract]
public class BotCondition
{
    [DataMember]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public EWildSpawnType Role { get; set; }

    [DataMember]
    public int Limit { get; set; }

    [DataMember]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public EBotDifficulty Difficulty { get; set; }
}