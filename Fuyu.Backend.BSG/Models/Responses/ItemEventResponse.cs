using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class ItemEventResponse
    {
        public ItemEventResponse()
        {
            ProfileChanges = [];
            InventoryWarnings = [];
        }

        [DataMember(Name = "profileChanges")]
        public Dictionary<MongoId, ProfileChange> ProfileChanges { get; set; }

        [DataMember(Name = "warnings")]
        public List<InventoryWarning> InventoryWarnings { get; set; }
    }
}