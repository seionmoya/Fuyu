using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class ToggleItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId Item { get; set; }

		[DataMember(Name = "value")]
		public bool Value { get; set; }
	}
}
