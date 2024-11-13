using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
    [DataContract]
    public class DeleteNoteItemEvent : BaseItemEvent
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }
    }
}
