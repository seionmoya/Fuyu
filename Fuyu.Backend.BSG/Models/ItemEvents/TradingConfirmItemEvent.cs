using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class TradingConfirmItemEvent : BaseItemEvent
{
    [DataMember(Name = "type")]
    public string Type { get; set; }

    [DataMember(Name = "tid")]
    public MongoId TraderId { get; set; }
}
