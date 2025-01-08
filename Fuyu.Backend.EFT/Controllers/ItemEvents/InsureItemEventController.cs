using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class InsureEventController : AbstractItemEventController<InsureItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public InsureEventController() : base("Insure")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, InsureItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var pmc = profile.Pmc;
            var inventoryItems = pmc.Inventory.Items;
            var insuredItems = new List<InsuredItem>(request.Items.Length);

            foreach (var itemIdToInsure in request.Items)
            {
                var itemInstance = inventoryItems.Find(i => i.Id == itemIdToInsure);
                if (itemInstance == null)
                {
                    context.AppendInventoryError("Failed to find one or more items on backend");

                    return Task.CompletedTask;
                }
                else
                {
                    insuredItems.Add(new InsuredItem { itemId = itemIdToInsure, tid = request.TraderId });
                }
            }

            pmc.InsuredItems.AddRange(insuredItems);

            return Task.CompletedTask;
        }
    }
}
