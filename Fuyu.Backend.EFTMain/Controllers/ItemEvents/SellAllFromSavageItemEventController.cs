using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFTMain.Controllers.ItemEvents;

public class SellAllFromSavageEventController : AbstractItemEventController<SellAllFromSavageItemEvent>
{
    public SellAllFromSavageEventController() : base("SellAllFromSavage")
    {
    }

    public override Task RunAsync(ItemEventContext context, SellAllFromSavageItemEvent request)
    {
        return Task.CompletedTask;
    }
}