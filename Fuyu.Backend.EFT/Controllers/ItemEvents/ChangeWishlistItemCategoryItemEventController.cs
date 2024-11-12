using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
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
