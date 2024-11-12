using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class RecodeItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId Item { get; set; }

		[DataMember(Name = "value")]
		public bool Encoded { get; set; }
	}
}
