using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class UnbindItemEventController : AbstractItemEventController<UnbindItemEvent>
    {
        public UnbindItemEventController() : base("Unbind")
        {
        }

        public override Task RunAsync(ItemEventContext context, UnbindItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);

            if (!profile.Pmc.Inventory.FastPanel.TryGetValue(request.Index, out var boundItemId))
            {
                context.AppendInventoryError("Nothing is bound to that slot on the backend");

                return Task.CompletedTask;
            }

            if (boundItemId != request.Item)
            {
                context.AppendInventoryError("Received item is not what is bound on the backend");

                return Task.CompletedTask;
            }

            profile.Pmc.Inventory.FastPanel.Remove(request.Index);

            return Task.CompletedTask;
        }
    }
}
