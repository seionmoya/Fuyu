using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Trading
{
    [DataContract]
    public class SupplyData
    {
        [DataMember(Name = "supplyNextTime")]
        public int SupplyNextTime { get; set; }

        [DataMember(Name = "prices")]
        public Dictionary<MongoId, double> MarketPrices { get; set; }

        [DataMember(Name = "currencyCourses")]
        public Dictionary<MongoId, double> CurrencyCourses { get; set; }
    }
}
