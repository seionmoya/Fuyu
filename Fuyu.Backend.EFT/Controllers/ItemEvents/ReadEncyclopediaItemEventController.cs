using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class ReadEncyclopediaEventController : ItemEventController<ReadEncyclopediaItemEvent>
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
