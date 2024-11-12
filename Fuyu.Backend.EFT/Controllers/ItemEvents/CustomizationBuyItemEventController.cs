using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class CustomizationBuyEventController : ItemEventController<CustomizationBuyItemEvent>
    {
        public CustomizationBuyEventController() : base("CustomizationBuy")
        {
        }

        public override Task RunAsync(ItemEventContext context, CustomizationBuyItemEvent request)
        {
            return Task.CompletedTask;
        }
    }
}
