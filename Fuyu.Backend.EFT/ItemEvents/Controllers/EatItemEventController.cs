using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class EatItemEventController : ItemEventController<EatItemEvent>
	{
		public EatItemEventController() : base("Eat")
		{
		}

		public override Task RunAsync(ItemEventContext context, EatItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var itemIndex = profile.Pmc.Inventory.Items.FindIndex(i => i.Id == request.Item);
			
			if (itemIndex == -1)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { Id = request.Item });
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

				return Task.CompletedTask;
			}

			var item = profile.Pmc.Inventory.Items[itemIndex];
			var foodDrink = item.GetUpdatable<ItemFoodDrinkComponent>(true);

			foodDrink.HpPercent -= request.Count;

			if (foodDrink.HpPercent <= 0f)
			{
				profile.Pmc.Inventory.Items.RemoveAt(itemIndex);
			}

			return Task.CompletedTask;
		}
	}
}
