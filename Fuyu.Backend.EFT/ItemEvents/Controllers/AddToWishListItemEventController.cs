using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class AddToWishListItemEventController : ItemEventController<AddToWishListItemEvent>
	{
		public AddToWishListItemEventController() : base("AddToWishList")
		{
		}

		public override Task RunAsync(ItemEventContext context, AddToWishListItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var wishList = profile.Pmc.GetWishList();

			foreach ((var itemId, var wishlistGroup) in request.Items)
			{
				wishList[itemId] = wishlistGroup;
			}

			return Task.CompletedTask;
		}
	}
}
