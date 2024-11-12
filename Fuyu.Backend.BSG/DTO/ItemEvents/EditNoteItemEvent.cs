using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles;

namespace Fuyu.Backend.BSG.ItemEvents.Models
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
