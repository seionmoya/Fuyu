﻿using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Common.IO;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class EatItemEventController : ItemEventController<EatItemEvent>
    {
        public EatItemEventController() : base("Eat")
        {
        }

        // This method only finds the item, as well as the index. Actually consuming/deleting the item needs to be done.
        public override Task RunAsync(ItemEventContext context, EatItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.FindItem(request.Item);

            if (item == null)
            {
                Terminal.WriteLine($"Failed to find item {request.Item}");
                return Task.CompletedTask;
            }

            var foodDrink = item.GetOrCreateUpdatable<ItemFoodDrinkComponent>();
            if (foodDrink == null)
            {
                Terminal.WriteLine("Could not find ItemFoodDrinkComponent on item: " + request.Item);
                return Task.CompletedTask;
            }

            foodDrink.HpPercent -= request.Count;
            if (foodDrink.HpPercent <= 0)
            {
                profile.Pmc.Inventory.RemoveItem(item);
            }

            return Task.CompletedTask;
        }
    }
}
