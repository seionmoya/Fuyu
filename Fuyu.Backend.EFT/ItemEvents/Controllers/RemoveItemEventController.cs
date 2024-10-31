using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
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
			var itemToRemove = profile.Pmc.Inventory.Items.Find(i => i._id == request.Item);

			if (itemToRemove == null)
			{
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}");

				return Task.CompletedTask;
			}

			profile.Pmc.Inventory.RemoveItem(itemToRemove);

			return Task.CompletedTask;
		}
	}
}