﻿using System;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Services;
using Fuyu.Common.Hashing;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFTMain.Controllers.ItemEvents;

public class RagFairBuyOfferItemEventController : AbstractItemEventController<RagFairBuyOfferItemEvent>
{
    private readonly EftOrm _eftOrm;
    private readonly RagfairService _ragfairService;
    private readonly ItemService _itemService;
    private readonly ItemFactoryService _itemFactoryService;

    public RagFairBuyOfferItemEventController() : base("RagFairBuyOffer")
    {
        _eftOrm = EftOrm.Instance;
        _ragfairService = RagfairService.Instance;
        _itemService = ItemService.Instance;
        _itemFactoryService = ItemFactoryService.Instance;
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
                var handOverItems = profile.Pmc.Inventory.GetItemAndChildren(_itemService, itemOffer.Id);

                if (handOverItems.Count == 0)
                {
                    throw new Exception($"Failed to find item {itemOffer.Id}");
                }

                var handOverItem = handOverItems[0];

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
                    profile.Pmc.Inventory.RemoveItem(handOverItem);
                    context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(handOverItem);
                }
                else
                {
                    context.Response.ProfileChanges[profile.Pmc._id].Items.Change.Add(handOverItem);
                }
            }

            var stacks = ItemFactoryService.Instance.CreateItemsFromTradeRequest(fleaOffer.Items, buyOffer.Count);

            foreach (var stack in stacks)
            {
                (int itemWidth, int itemHeight) = _itemService.CalculateItemSize(stack);
                var targetLocation = profile.Pmc.Inventory.GetNextFreeSlot(_itemService, itemWidth, itemHeight, out string gridName);

                if (targetLocation == null)
                {
                    throw new Exception("No room for item");
                }

                var rootItem = stack[0];
                rootItem.Location = targetLocation;
                rootItem.SlotId = gridName;
                rootItem.ParentId = profile.Pmc.Inventory.Stash;

                profile.Pmc.Inventory.AddItems(_itemService, ItemFactoryService.Instance,
                    stack);

                context.Response.ProfileChanges[profile.Pmc._id].Items.New.AddRange(stack);
            }
        }

        return Task.CompletedTask;
    }
}