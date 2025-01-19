using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Trading;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class TradingConfirmSellItemEvent : TradingConfirmItemEvent
{
    [DataMember(Name = "items")]
    public TradingItemScheme[] Items { get; set; }

    [DataMember(Name = "price")]
    public int Price { get; set; }
}
