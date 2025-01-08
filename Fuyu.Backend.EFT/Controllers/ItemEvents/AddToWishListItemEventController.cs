using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class AddToWishListItemEventController : AbstractItemEventController<AddToWishListItemEvent>
    {
        public AddToWishListItemEventController() : base("AddToWishList")
        {
        }

        public override Task RunAsync(ItemEventContext context, AddToWishListItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var wishList = profile.Pmc.GetWishList();

            foreach ((var itemId, var wishlistGroup) in request.Items)
            {
                wishList[itemId] = wishlistGroup;
            }

            return Task.CompletedTask;
        }
    }
}
