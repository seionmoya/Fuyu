using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents;

public class RemoveFromWishListItemEventController : AbstractItemEventController<RemoveFromWishListItemEvent>
{
    private readonly EftOrm _eftOrm;

    public RemoveFromWishListItemEventController() : base("RemoveFromWishList")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(ItemEventContext context, RemoveFromWishListItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);
        var wishList = profile.Pmc.GetWishList();

        foreach (var itemToRemove in request.Items)
        {
            wishList.Remove(itemToRemove);
        }

        return Task.CompletedTask;
    }
}