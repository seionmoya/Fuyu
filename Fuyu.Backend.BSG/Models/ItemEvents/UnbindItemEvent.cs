using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class UnbindItemEvent : BaseItemEvent
    {
        [DataMember(Name = "item")]
        public MongoId Item { get; set; }

        [DataMember(Name = "index")]
        public string Index { get; set; }
    }
}
