using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class CustomizationBuyEventController : AbstractItemEventController<CustomizationBuyItemEvent>
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