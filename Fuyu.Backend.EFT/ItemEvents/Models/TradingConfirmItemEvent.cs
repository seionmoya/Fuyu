using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
    [DataContract]
    public class TradingConfirmItemEvent : BaseItemEvent
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "tid")]
        public MongoId TraderId { get; set; }

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
}
