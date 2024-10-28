using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.EFT.ItemEvents.Models;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using System.Threading.Tasks;
using System;
using Fuyu.Backend.BSG.ItemEvents.Models;
using System.Linq;

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
            var account = EftOrm.GetAccount(context.SessionId);
            var profile = EftOrm.GetProfile(account.PveId);
            var inventory = profile.Pmc.Inventory;
			var roubles = inventory.GetItemsByTemplate("5449016a4bdc2d6f028b456f").FirstOrDefault();

            if (roubles == null)
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

			roubles.upd.StackObjectsCount += request.Price;
			context.Response.ProfileChanges[profile.Pmc._id].Items.Change.Add(roubles);

			return Task.CompletedTask;
        }
    }
}
