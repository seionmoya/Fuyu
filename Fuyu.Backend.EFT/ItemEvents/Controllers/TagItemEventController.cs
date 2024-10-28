using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class TagItemEventController : ItemEventController<TagItemEvent>
	{
		public TagItemEventController() : base("Tag")
		{
		}

		public override Task RunAsync(ItemEventContext context, TagItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var item = profile.Pmc.Inventory.items.FirstOrDefault(i => i._id == request.Item);

			if (item == null)
			{
				context.AppendInventoryError("Item not found on backend");

				return Task.CompletedTask;
			}

			var tag = item.GetUpd<ItemTagComponent>();
			tag.Name = request.Name;
			tag.Color = request.Color;

			return Task.CompletedTask;
		}
	}
}
