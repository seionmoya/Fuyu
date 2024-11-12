using System;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class TradingConfirmEventController : ItemEventController<TradingConfirmItemEvent>
    {
        public TradingConfirmEventController() : base("TradingConfirm")
        {
        }

        public override Task RunAsync(ItemEventContext context, TradingConfirmItemEvent request)
        {
            switch (request.Type)
            {
                case "sell_to_trader":
                {
                    return SellToTrader(context, request);
                }
            }

            throw new Exception($"Unhandled TradingConfirm.Type '{request.Type}'");
        }

        public Task SellToTrader(ItemEventContext context, TradingConfirmItemEvent request)
        {
            var profile = EftOrm.GetActiveProfile(context.SessionId);
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
    }
}
