using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class RecodeItemEventController : ItemEventController<RecodeItemEvent>
	{
		public RecodeItemEventController() : base("Recode")
		{
		}

		public override Task RunAsync(ItemEventContext context, RecodeItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var item = profile.Pmc.Inventory.items.FirstOrDefault(i => i._id == request.Item);
			
			if (item == null)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { _id = request.Item });
				context.AppendInventoryError($"Failed to find item on backend: {request.Item}, removing it");

				return Task.CompletedTask;
			}

			item.GetUpd<ItemRecodableComponent>().IsEncoded = request.Encoded;

			return Task.CompletedTask;
		}
	}
}
