using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
    [DataContract]
    public class InsureItemEvent : BaseItemEvent
    {
        [DataMember(Name = "items")]
        public MongoId[] Items { get; set; }

        [DataMember(Name = "tid")]
        public MongoId TraderId { get; set; }
    }
}
