using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents;

public class InterGameTransferEventController : AbstractItemEventController<InterGameTransferItemEvent>
{
    public InterGameTransferEventController() : base("InterGameTransfer")
    {
    }

    public override Task RunAsync(ItemEventContext context, InterGameTransferItemEvent request)
    {
        return Task.CompletedTask;
    }
}