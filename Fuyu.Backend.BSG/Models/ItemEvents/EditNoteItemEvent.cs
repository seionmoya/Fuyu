using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class EditNoteItemEvent : BaseItemEvent
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }

        [DataMember(Name = "note")]
        public Note Note { get; set; }
    }
}
