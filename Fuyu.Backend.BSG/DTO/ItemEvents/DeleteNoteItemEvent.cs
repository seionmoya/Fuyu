using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class DeleteNoteItemEvent : BaseItemEvent
	{
		[DataMember(Name = "index")]
		public int Index { get; set; }
	}
}
