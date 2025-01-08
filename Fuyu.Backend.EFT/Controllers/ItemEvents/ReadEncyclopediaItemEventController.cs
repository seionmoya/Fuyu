using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class ReadEncyclopediaEventController : AbstractItemEventController<ReadEncyclopediaItemEvent>
    {
        public ReadEncyclopediaEventController() : base("ReadEncyclopedia")
        {
        }

        public override Task RunAsync(ItemEventContext context, ReadEncyclopediaItemEvent request)
        {
            return Task.CompletedTask;
        }
    }
}
