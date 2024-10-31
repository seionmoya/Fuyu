using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class ToggleItemEventController : ItemEventController<ToggleItemEvent>
	{
		public ToggleItemEventController() : base("Toggle")
		{
		}

		public override Task RunAsync(ItemEventContext context, ToggleItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var item = profile.Pmc.Inventory.Items.Find(i => i._id == request.Item);
			
			if (item == null)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { _id = request.Item });
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

				return Task.CompletedTask;
			}

			// TODO: I'm pretty sure we should have an ItemTogglableComponent?
			// -- nexus4880, 2024-10-27

			return Task.CompletedTask;
		}
	}
}
