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
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var wishList = profile.Pmc.GetWishList();

			wishList[request.Item] = request.Category;

			return Task.CompletedTask;
		}
	}
}
