using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
	public class RemoveItemEventController : ItemEventController<RemoveItemEvent>
	{
		public RemoveItemEventController() : base("Remove")
		{
		}

		public override Task RunAsync(ItemEventContext context, RemoveItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var itemToRemove = profile.Pmc.Inventory.Items.Find(i => i.Id == request.Item);

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