using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class PlacedQuestItem
{
    [DataMember(Name = "QuestId")]
    public MongoId QuestId { get; set; }

    [DataMember(Name = "ItemId")]
    public MongoId ItemId { get; set; }

    [DataMember(Name = "ZoneId")]
    public string ZoneId { get; set; }
}