using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class ChangeWishlistItemCategoryItemEventController : ItemEventController<ChangeWishlistItemCategoryItemEvent>
	{
		public ChangeWishlistItemCategoryItemEventController() : base("ChangeWishlistItemCategory")
		{
		}

		public override Task RunAsync(ItemEventContext context, ChangeWishlistItemCategoryItemEvent request)
		{
			return Task.CompletedTask;
		}
	}
}
