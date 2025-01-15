using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Services;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;
using Newtonsoft.Json;

namespace Fuyu.Backend.EFTMain.Controllers.ItemEvents;

public class RagFairBuyOfferItemEventController : AbstractItemEventController<RagFairBuyOfferItemEvent>
{
    private readonly EftOrm _eftOrm;
    private readonly RagfairService _ragfairService;

    public RagFairBuyOfferItemEventController() : base("RagFairBuyOffer")
    {
        _eftOrm = EftOrm.Instance;
        _ragfairService = RagfairService.Instance;
    }

    // TODO: logic for buying is wrong
    public override Task RunAsync(ItemEventContext context, RagFairBuyOfferItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);

        foreach (var buyOffer in request.BuyOffers)
        {
            var fleaOffer = _ragfairService.GetOffer(buyOffer.Id);
            
            if (fleaOffer == null)
            {
                throw new Exception("Failed to find offer");
            }

            if (fleaOffer.RootItem.Updatable.StackObjectsCount < buyOffer.Count)
            {
                throw new Exception("User wants to buy more than available");
            }
            
            fleaOffer.RootItem.Updatable.StackObjectsCount -= buyOffer.Count;

            if (fleaOffer.RootItem.Updatable.StackObjectsCount <= 0)
            {
                _ragfairService.RemoveOffer(fleaOffer);
            }

            // Remove items from player stash
            foreach (var itemOffer in buyOffer.ItemOffers)
            {
                var handOverItem = profile.Pmc.Inventory.Items.Find(i => i.Id == itemOffer.Id);

                if (handOverItem == null)
                {
                    throw new Exception($"Failed to find item {itemOffer.Id}");
                }

                if (!handOverItem.Updatable.StackObjectsCount.HasValue)
                {
                    throw new Exception($"Item {itemOffer.Id} has no stack objects count");
                }
                
                if (handOverItem.Updatable.StackObjectsCount < itemOffer.Count)
                {
                    throw new Exception("Stack count mismatch");
                }
                
                handOverItem.Updatable.StackObjectsCount -= itemOffer.Count;
                
                if (handOverItem.Updatable.StackObjectsCount.Value <= 0)
                {
                    profile.Pmc.Inventory.Items.Remove(handOverItem);
                    context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(handOverItem);
                }
                else
                {
                    context.Response.ProfileChanges[profile.Pmc._id].Items.Change.Add(handOverItem);
                }
            }

            var clonedRootItem = Json.Clone<ItemInstance>(fleaOffer.RootItem);
            var properties = ItemFactoryService.Instance.GetItemProperties<ItemProperties>(clonedRootItem.TemplateId);
            clonedRootItem.Id = new MongoId(true);
            clonedRootItem.Updatable.StackObjectsCount = buyOffer.Count;
            clonedRootItem.Location = profile.Pmc.Inventory.GetNextFreeSlot(properties.Width, properties.Height, out string gridName);
            clonedRootItem.SlotId = gridName;
            clonedRootItem.ParentId = profile.Pmc.Inventory.Stash;
            
            profile.Pmc.Inventory.Items.Add(clonedRootItem);
            context.Response.ProfileChanges[profile.Pmc._id].Items.New.Add(clonedRootItem);
        }

        return Task.CompletedTask;
    }
}