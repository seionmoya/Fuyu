using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class ApplyInventoryChangesItemEventController : ItemEventController<ApplyInventoryChangesEvent>
    {
        private readonly EftOrm _eftOrm;

        public ApplyInventoryChangesItemEventController() : base("ApplyInventoryChanges")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, ApplyInventoryChangesEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var profileItems = new ThreadDictionary<MongoId, ItemInstance>(profile.Pmc.Inventory.Items.ToDictionary(i => i.Id, i => i));

            Parallel.ForEach(request.ChangedItems, changedItem =>
            {
                if (profileItems.TryGet(changedItem.Id, out var item))
                {
                    item.SlotId = changedItem.SlotId;
                    item.Location = changedItem.Location;
                    item.ParentId = changedItem.ParentId;
                }
            });

            return Task.CompletedTask;
        }
    }
}
