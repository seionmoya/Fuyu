using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
	[DataContract]
	public class AddToWishListItemEvent : BaseItemEvent
	{
		[DataMember(Name = "items")]
		public Dictionary<MongoId, EWishlistGroup> Items { get; set; }
	}
}
