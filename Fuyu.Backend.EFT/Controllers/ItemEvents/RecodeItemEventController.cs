using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class RecodeItemEventController : ItemEventController<RecodeItemEvent>
    {
        public RecodeItemEventController() : base("Recode")
        {
        }

        public override Task RunAsync(ItemEventContext context, RecodeItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.Items.Find(i => i.Id == request.Item);

            if (item == null)
            {
                context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { Id = request.Item });
                context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

                return Task.CompletedTask;
            }

            item.GetUpdatable<ItemRecodableComponent>().IsEncoded = request.Encoded;

            return Task.CompletedTask;
        }
    }
}
