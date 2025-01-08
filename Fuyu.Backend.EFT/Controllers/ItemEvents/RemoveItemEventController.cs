using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class RemoveItemEventController : ItemEventController<RemoveItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public RemoveItemEventController() : base("Remove")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, RemoveItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var itemToRemove = profile.Pmc.Inventory.Items.Find(i => i.Id == request.Item);

            if (itemToRemove == null)
            {
                context.AppendInventoryError($"Failed to find item on backend: {request.Item}");

                return Task.CompletedTask;
            }

            profile.Pmc.Inventory.RemoveItem(itemToRemove);

            return Task.CompletedTask;
        }
    }
}