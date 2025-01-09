using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Common.IO;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class HealItemEventController : AbstractItemEventController<HealItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public HealItemEventController() : base("Heal")
        {
            _eftOrm = EftOrm.Instance;
        }

        // This method only finds the item, as well as the index. Actually consuming/deleting the item needs to be done.
        public override Task RunAsync(ItemEventContext context, HealItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.FindItem(request.Item);

            if (item == null)
            {
                Terminal.WriteLine($"Failed to find item {request.Item}");
                return Task.CompletedTask;
            }

            var medKit = item.GetOrCreateUpdatable<ItemMedKitComponent>();
            if (medKit == null)
            {
                Terminal.WriteLine("Could not find medKit on item: " + request.Item);
                return Task.CompletedTask;
            }

            medKit.HpResource -= request.Count;
            if (medKit.HpResource <= 0)
            {
                profile.Pmc.Inventory.RemoveItem(item);
            }

            var bodyPart = profile.Pmc.Health.GetBodyPart(request.BodyPart);

            return Task.CompletedTask;
        }
    }
}
