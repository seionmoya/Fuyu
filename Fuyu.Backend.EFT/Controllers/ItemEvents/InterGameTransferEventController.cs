using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class InterGameTransferEventController : ItemEventController<InterGameTransferItemEvent>
    {
        public InterGameTransferEventController() : base("InterGameTransfer")
        {
        }

        public override Task RunAsync(ItemEventContext context, InterGameTransferItemEvent request)
        {
            return Task.CompletedTask;
        }
    }
}
