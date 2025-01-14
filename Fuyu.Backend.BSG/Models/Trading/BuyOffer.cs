﻿using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading;

[DataContract]
public class BuyOffer
{
    [DataMember(Name = "id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "items")]
    public ItemOffer[] Items { get; set; }
}