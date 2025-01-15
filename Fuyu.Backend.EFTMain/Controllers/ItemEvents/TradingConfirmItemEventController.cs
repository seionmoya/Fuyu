using System;
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

namespace Fuyu.Backend.EFTMain.Controllers.ItemEvents;

public class TradingConfirmEventController : AbstractItemEventController<TradingConfirmItemEvent>
{
    private readonly EftOrm _eftOrm;
    private readonly TraderOrm _traderOrm;

    public TradingConfirmEventController() : base("TradingConfirm")
    {
        _eftOrm = EftOrm.Instance;
        _traderOrm = TraderOrm.Instance;
    }

    public override Task RunAsync(ItemEventContext context, TradingConfirmItemEvent request)
    {
        Terminal.WriteLine(context.Data.ToString());
        
        switch (request.Type)
        {
            case "sell_to_trader":
                {
                    return SellToTrader(context, context.GetData<TradingConfirmSellItemEvent>());
                }
            case "buy_from_trader":
                {
                    return BuyFromTrader(context, context.GetData<TradingConfirmBuyItemEvent>());
                }
        }
        
        throw new Exception($"Unhandled TradingConfirm.Type '{request.Type}'");
    }

    public Task SellToTrader(ItemEventContext context, TradingConfirmSellItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);
        var inventory = profile.Pmc.Inventory;
        var roubles = inventory.GetItemsByTemplate("5449016a4bdc2d6f028b456f");

        if (roubles.Count == 0)
        {
            context.AppendInventoryError("You don't have any roubles and I'm too dumb to add items so I can't pay you");

            return Task.CompletedTask;
        }

        foreach (var tradingItem in request.Items)
        {
            try
            {
                var removedItems = inventory.RemoveItem(tradingItem.Id);
                context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(removedItems[0]);
            }
            catch (Exception ex)
            {
                context.AppendInventoryError(ex.Message);

                return Task.CompletedTask;
            }
        }

        var roublesItem = roubles[0];
        roublesItem.Updatable.StackObjectsCount += request.Price;
        context.Response.ProfileChanges[profile.Pmc._id].Items.Change.Add(roublesItem);

        return Task.CompletedTask;
    }

    public Task BuyFromTrader(ItemEventContext context, TradingConfirmBuyItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);
        var traderAssort = _traderOrm.GetTraderAssort(request.TraderId);
        var itemToBuy = traderAssort.Items.Find(i => i.Id == request.ItemId);
        
        if (itemToBuy == null)
        {
            throw new Exception("Failed to find item to buy");
        }
        
        if (!profile.Pmc.TradersInfo.HasValue
            || !profile.Pmc.TradersInfo.Value.IsValue1
            || !profile.Pmc.TradersInfo.Value.Value1.TryGetValue(request.TraderId, out var traderInfo))
        {
            throw new Exception("Failed to get trader info");
        }

        var properties = ItemFactoryService.Instance.GetItemProperties<ItemProperties>(itemToBuy.TemplateId);
        var targetLocation = profile.Pmc.Inventory.GetNextFreeSlot(properties.Width, properties.Height, out string gridName);
        
        if (targetLocation == null)
        {
            throw new Exception("No room for item");
        }
        
        foreach (var tradingItem in request.Items)
        {
            var itemInstance = profile.Pmc.Inventory.Items.Find(i => i.Id == tradingItem.Id);
            
            if (itemInstance == null)
            {
                throw new Exception("Failed to find item");
            }

            itemInstance.Updatable.StackObjectsCount -= tradingItem.Count;

            // TODO: this needs to be something like GetItemWorth()
            traderInfo.salesSum += tradingItem.Count;

            if (itemInstance.Updatable.StackObjectsCount <= 0)
            {
                profile.Pmc.Inventory.Items.Remove(itemInstance);
                context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(itemInstance);
            }
            else
            {
                context.Response.ProfileChanges[profile.Pmc._id].Items.Change.Add(itemInstance);
            }
        }

        var clonedRootItem = Json.Clone<ItemInstance>(itemToBuy);
        clonedRootItem.Id = new MongoId(true);
        clonedRootItem.Updatable.StackObjectsCount = request.Count;
        clonedRootItem.Location = targetLocation;
        clonedRootItem.SlotId = gridName;
        clonedRootItem.ParentId = profile.Pmc.Inventory.Stash;

        profile.Pmc.Inventory.Items.Add(clonedRootItem);
        context.Response.ProfileChanges[profile.Pmc._id].Items.New.Add(clonedRootItem);

        return Task.CompletedTask;
    }
}