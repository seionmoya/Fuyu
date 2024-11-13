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
            var profile = EftOrm.GetActiveProfile(context.SessionId);
            var wishList = profile.Pmc.GetWishList();

            foreach ((var itemId, var wishlistGroup) in request.Items)
            {
                wishList[itemId] = wishlistGroup;
            }

            return Task.CompletedTask;
        }
    }
}
