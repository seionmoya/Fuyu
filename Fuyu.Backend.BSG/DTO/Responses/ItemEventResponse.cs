using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.DTO.Responses
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
