using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class MoveItemEventController : AbstractItemEventController<MoveItemEvent>
    {
        public MoveItemEventController() : base("Move")
        {
        }

        public override Task RunAsync(ItemEventContext context, MoveItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.FindItem(request.Item);
            if (item != null)
            {
                item.Location = request.To.Location;
                item.ParentId = request.To.Id;
                item.SlotId = request.To.Container;
                Terminal.WriteLine($"{request.Item} moved to {request.To.Location}");
            }
            else
            {
                Terminal.WriteLine($"Failed to find {request.Item} in inventory");
            }

            return Task.CompletedTask;
        }
    }
}
