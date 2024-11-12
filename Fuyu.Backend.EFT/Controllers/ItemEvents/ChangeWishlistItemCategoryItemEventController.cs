using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class ChangeWishlistItemCategoryItemEventController : ItemEventController<ChangeWishlistItemCategoryItemEvent>
	{
		public ChangeWishlistItemCategoryItemEventController() : base("ChangeWishlistItemCategory")
		{
		}

		public override Task RunAsync(ItemEventContext context, ChangeWishlistItemCategoryItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var wishList = profile.Pmc.GetWishList();

			wishList[request.Item] = request.Category;

			return Task.CompletedTask;
		}
	}
}
