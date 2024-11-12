using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class SellAllFromSavageEventController : ItemEventController<SellAllFromSavageItemEvent>
    {
        public SellAllFromSavageEventController() : base("SellAllFromSavage")
        {
        }

        public override Task RunAsync(ItemEventContext context, SellAllFromSavageItemEvent request)
        {
            return Task.CompletedTask;
        }
    }
}
