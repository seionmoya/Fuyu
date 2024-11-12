using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.DTO.Items;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
	public class ToggleItemEventController : ItemEventController<ToggleItemEvent>
	{
		public ToggleItemEventController() : base("Toggle")
		{
		}

		public override Task RunAsync(ItemEventContext context, ToggleItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var item = profile.Pmc.Inventory.Items.Find(i => i.Id == request.Item);
			
			if (item == null)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { Id = request.Item });
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

				return Task.CompletedTask;
			}

			// TODO: I'm pretty sure we should have an ItemTogglableComponent?
			// -- nexus4880, 2024-10-27

			return Task.CompletedTask;
		}
	}
}
