using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class AddToWishListItemEvent : BaseItemEvent
    {
        [DataMember(Name = "items")]
        public Dictionary<MongoId, EWishlistGroup> Items { get; set; }
    }
}