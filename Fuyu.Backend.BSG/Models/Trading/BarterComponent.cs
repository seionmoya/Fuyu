using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading
{
    [DataContract]
    public class BarterComponent
    {
        [DataMember(Name = "_tpl")]
        public MongoId Template { get; set; }

        [DataMember(Name = "count")]
        public double Count { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "side")]
        public EDogtagExchangeSide Side { get; set; }

        [DataMember(Name = "onlyFunctional")]
        public bool OnlyFunctional { get; set; }
    }
}