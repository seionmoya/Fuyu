using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
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
