using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
	[DataContract]
	public class ChangeWishlistItemCategoryItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId Item { get; set; }

		[DataMember(Name = "category")]
		public EWishlistGroup Category { get; set; }
	}
}
