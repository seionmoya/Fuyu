﻿using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;
using Fuyu.Common.Collections;
using System.Linq;
using System.Threading.Tasks;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class ApplyInventoryChangesItemEventController : ItemEventController<ApplyInventoryChangesEvent>
    {
        public ApplyInventoryChangesItemEventController() : base("ApplyInventoryChanges")
        {
        }

        public override Task RunAsync(ItemEventContext context, ApplyInventoryChangesEvent request)
        {
            var profile = EftOrm.GetActiveProfile(context.SessionId);
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
