using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class ApplyInventoryChangesEvent : BaseItemEvent
    {
        [DataMember(Name = "changedItems")]
        public ItemInstance[] ChangedItems { get; set; }
    }
}
