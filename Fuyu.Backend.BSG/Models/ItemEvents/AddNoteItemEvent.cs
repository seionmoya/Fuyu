using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class AddNoteItemEvent : BaseItemEvent
    {
        [DataMember(Name = "note")]
        public Note Note { get; set; }
    }
}