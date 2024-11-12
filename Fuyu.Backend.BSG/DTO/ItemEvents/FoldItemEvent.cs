using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Models
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