using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class RemoveItemEventController : ItemEventController<RemoveItemEvent>
	{
		public RemoveItemEventController() : base("Remove")
		{
		}

		public override Task RunAsync(ItemEventContext context, RemoveItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var itemToRemove = profile.Pmc.Inventory.items.FirstOrDefault(i => i._id == request.Item);

			if (itemToRemove == null)
			{
				context.AppendInventoryError("Item was not found on backend");

				return Task.CompletedTask;
			}

			profile.Pmc.Inventory.RemoveItem(itemToRemove);

			return Task.CompletedTask;
		}
	}
}