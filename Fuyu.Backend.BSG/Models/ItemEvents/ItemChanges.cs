using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class ItemChanges
    {
        public ItemChanges()
        {
            New = [];
            Change = [];
            Delete = [];
        }

        [DataMember(Name = "new")]
        public List<ItemInstance> New { get; set; }

        [DataMember(Name = "change")]
        public List<ItemInstance> Change { get; set; }

        [DataMember(Name = "del")]
        public List<ItemInstance> Delete { get; set; }
    }
}
