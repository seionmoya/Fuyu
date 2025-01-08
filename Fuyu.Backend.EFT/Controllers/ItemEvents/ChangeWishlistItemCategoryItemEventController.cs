using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class ChangeWishlistItemCategoryItemEventController : AbstractItemEventController<ChangeWishlistItemCategoryItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public ChangeWishlistItemCategoryItemEventController() : base("ChangeWishlistItemCategory")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, ChangeWishlistItemCategoryItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var wishList = profile.Pmc.GetWishList();

            wishList[request.Item] = request.Category;

            return Task.CompletedTask;
        }
    }
}
