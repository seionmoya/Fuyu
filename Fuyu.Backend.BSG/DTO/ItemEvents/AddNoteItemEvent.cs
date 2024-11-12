using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class AddNoteItemEvent : BaseItemEvent
	{
		[DataMember(Name = "note")]
		public Note Note { get; set; }
	}
}
