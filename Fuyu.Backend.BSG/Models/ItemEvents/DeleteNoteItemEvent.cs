using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class DeleteNoteItemEvent : BaseItemEvent
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }
    }
}