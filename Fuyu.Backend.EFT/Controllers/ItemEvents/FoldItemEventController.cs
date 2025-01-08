using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class FoldItemEventController : ItemEventController<FoldItemEvent>
    {
        public FoldItemEventController() : base("Fold")
        {
        }

        public override Task RunAsync(ItemEventContext context, FoldItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.Items.Find(i => i.Id == request.ItemId);

            if (item == null)
            {
                context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { Id = request.ItemId });
                context.AppendInventoryError($"Failed to find item on backend: {request.ItemId}, removing it");

                return Task.CompletedTask;
            }

            item.GetOrCreateUpdatable<ItemFoldableComponent>().Folded = request.Value;

            return Task.CompletedTask;
        }
    }
}