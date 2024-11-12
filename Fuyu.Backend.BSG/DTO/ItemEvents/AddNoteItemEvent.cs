using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
	[DataContract]
	public class AddNoteItemEvent : BaseItemEvent
	{
		[DataMember(Name = "note")]
		public Note Note { get; set; }
	}
}
