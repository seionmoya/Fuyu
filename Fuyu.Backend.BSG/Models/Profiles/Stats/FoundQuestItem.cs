using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class FoundQuestItem
{
    [DataMember(Name = "QuestId")]
    public MongoId QuestId { get; set; }

    [DataMember(Name = "ItemId")]
    public MongoId ItemId { get; set; }
}