using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.BSG.DTO.Items;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class RecodeItemEventController : ItemEventController<RecodeItemEvent>
	{
		public RecodeItemEventController() : base("Recode")
		{
		}

		public override Task RunAsync(ItemEventContext context, RecodeItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);
			var item = profile.Pmc.Inventory.Items.Find(i => i.Id == request.Item);
			
			if (item == null)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { Id = request.Item });
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

				return Task.CompletedTask;
			}

			item.GetUpdatable<ItemRecodableComponent>().IsEncoded = request.Encoded;

			return Task.CompletedTask;
		}
	}
}
