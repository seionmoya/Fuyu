using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class TraderRepairEventController : ItemEventController<TraderRepairItemEvent>
    {
        public TraderRepairEventController() : base("TraderRepair")
        {
        }

        public override Task RunAsync(ItemEventContext context, TraderRepairItemEvent request)
        {
            return Task.CompletedTask;
        }
    }
}
