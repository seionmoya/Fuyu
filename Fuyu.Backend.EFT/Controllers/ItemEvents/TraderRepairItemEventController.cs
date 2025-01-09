using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class TraderRepairEventController : AbstractItemEventController<TraderRepairItemEvent>
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