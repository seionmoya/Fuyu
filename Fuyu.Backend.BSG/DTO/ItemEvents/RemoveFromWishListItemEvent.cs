using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class RemoveFromWishListItemEvent : BaseItemEvent
	{
		[DataMember(Name = "items")]
		public MongoId[] Items { get; set; }
	}
}
