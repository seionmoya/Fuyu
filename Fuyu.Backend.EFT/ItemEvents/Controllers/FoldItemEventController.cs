using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class FoldItemEventController : ItemEventController<FoldItemEvent>
	{
		public FoldItemEventController() : base("Fold")
		{
		}

		public override Task RunAsync(ItemEventContext context, FoldItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var item = profile.Pmc.Inventory.Items.Find(i => i._id == request.ItemId);
			if (item == null)
			{
				context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(new ItemInstance { _id = request.ItemId });
				context.AppendInventoryError($"Failed to find item on backend: {request.ItemId}, removing it");

				return Task.CompletedTask;
			}

			// NOTE: I would like to not have to do this...
			// potentially have an extension or helper method
			// that would add the updatable for us if null
			// -- nexus4880, 2024-10-24

			/* Leaving old code here for later discussion -- nexus4880, 2024-10-27*/

			item.upd ??= new ItemUpdatable();
			item.upd.Foldable ??= new ItemFoldableComponent();
			item.upd.Foldable.Folded = request.Value;
			

			item.GetUpdatable<ItemFoldableComponent>().Folded = request.Value;

			return Task.CompletedTask;
		}
	}
}