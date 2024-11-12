using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class FoldItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId ItemId { get; set; }

		[DataMember(Name = "value")]
		public bool Value { get; set; }
	}
}