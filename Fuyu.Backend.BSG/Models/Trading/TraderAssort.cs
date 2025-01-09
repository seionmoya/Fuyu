using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading
{
    [DataContract]
    public class TraderAssort
    {
        [DataMember(Name = "exchange_rate")]
        public double ExchangeRate { get; set; }

        [DataMember(Name = "items")]
        public ItemInstance[] Items { get; set; }

        [DataMember(Name = "barter_scheme")]
        public Dictionary<MongoId, BarterComponent[][]> BarterScheme { get; set; }

        [DataMember(Name = "loyal_level_items")]
        public Dictionary<MongoId, int> LoyaltyLevelItems { get; set; }

        [DataMember(Name = "nextResupply")]
        public int NextResupply { get; set; }
    }
}