using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class RemoveFromWishListItemEventController : ItemEventController<RemoveFromWishListItemEvent>
	{
		public RemoveFromWishListItemEventController() : base("RemoveFromWishList")
		{
		}

		public override Task RunAsync(ItemEventContext context, RemoveFromWishListItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var wishList = profile.Pmc.GetWishList();

			foreach (var itemToRemove in request.Items)
			{
				wishList.Remove(itemToRemove);
			}

			return Task.CompletedTask;
		}
	}
}
