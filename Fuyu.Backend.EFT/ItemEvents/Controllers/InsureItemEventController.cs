using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class InsureEventController : ItemEventController<InsureItemEvent>
	{
		public InsureEventController() : base("Insure")
		{
		}

		public override Task RunAsync(ItemEventContext context, InsureItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var pmc = profile.Pmc;
			var inventoryItems = pmc.Inventory.items;
			var insuredItems = new List<InsuredItem>(request.Items.Length);

			foreach (var itemIdToInsure in request.Items)
			{
				var itemInstance = inventoryItems.Find(i => i._id == itemIdToInsure);
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
