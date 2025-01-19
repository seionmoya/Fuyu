using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class TradingConfirmBuyItemEvent : TradingConfirmItemEvent
{
    [DataMember(Name = "item_id")]
    public MongoId ItemId { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "scheme_id")]
    public int SchemeId { get; set; }

    [DataMember(Name = "scheme_items")]
    public TradingItemScheme[] Items { get; set; }
}