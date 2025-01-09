using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class RemoveFromWishListItemEvent : BaseItemEvent
    {
        [DataMember(Name = "items")]
        public MongoId[] Items { get; set; }
    }
}