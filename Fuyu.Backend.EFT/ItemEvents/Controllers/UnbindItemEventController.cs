using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class UnbindItemEventController : ItemEventController<UnbindItemEvent>
	{
		public UnbindItemEventController() : base("Unbind")
		{
		}

		public override Task RunAsync(ItemEventContext context, UnbindItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);

			if (!profile.Pmc.Inventory.FastPanel.TryGetValue(request.Index, out var boundItemId))
			{
				context.AppendInventoryError("Nothing is bound to that slot on the backend");

				return Task.CompletedTask;
			}

			if (boundItemId != request.Item)
			{
				context.AppendInventoryError("Received item is not what is bound on the backend");

				return Task.CompletedTask;
			}

			profile.Pmc.Inventory.FastPanel.Remove(request.Index);

			return Task.CompletedTask;
		}
	}
}
