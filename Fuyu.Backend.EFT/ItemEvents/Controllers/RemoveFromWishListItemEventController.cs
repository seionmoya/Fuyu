using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class RemoveFromWishListItemEventController : ItemEventController<RemoveFromWishListItemEvent>
	{
		public RemoveFromWishListItemEventController() : base("RemoveFromWishList")
		{
		}

		public override Task RunAsync(ItemEventContext context, RemoveFromWishListItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var wishList = profile.Pmc.WishList.Value1;

			foreach (var itemToRemove in request.Items)
			{
				wishList.Remove(itemToRemove);
			}

			return Task.CompletedTask;
		}
	}
}
