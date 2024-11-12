using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class RemoveItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId Item { get; set; }
	}
}