﻿using System.Runtime.Serialization;
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

[DataContract]
public class TradingConfirmSellItemEvent : TradingConfirmItemEvent
{
    [DataMember(Name = "items")]
    public TradingItemScheme[] Items { get; set; }

    [DataMember(Name = "price")]
    public int Price { get; set; }
}

[DataContract]
public class TradingItemScheme
{
    [DataMember(Name = "id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "scheme_id")]
    public int SchemeId { get; set; }
}