using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class EatItemEventController : ItemEventController<EatItemEvent>
    {
        public EatItemEventController() : base("Eat")
        {
        }

        // This method only finds the item, as well as the index. Actually consuming/deleting the item needs to be done.
        public override Task RunAsync(ItemEventContext context, EatItemEvent request)
        {
            var profile = EftOrm.GetActiveProfile(context.SessionId);
            var index = 0;
            ItemInstance item = null;

            foreach (var _item in profile.Pmc.Inventory.Items)
            {
                if (_item.Id == request.Item)
                {
                    item = _item;
                    break;
                }

                index++;
            }

            if (item == null)
            {
                Terminal.WriteLine($"Failed to find item {request.Item}");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
