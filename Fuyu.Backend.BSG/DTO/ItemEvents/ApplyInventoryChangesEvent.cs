using Fuyu.Backend.BSG.DTO.Items;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
    [DataContract]
    public class ApplyInventoryChangesEvent : BaseItemEvent
    {
        [DataMember(Name = "changedItems")]
        public ItemInstance[] ChangedItems { get; set; }
    }
}
