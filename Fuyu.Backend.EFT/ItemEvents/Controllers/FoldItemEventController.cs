using System;
using System.Linq;
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
			var item = profile.Pmc.Inventory.items.FirstOrDefault(i => i._id == request.ItemId);
			if (item == null)
			{
				throw new Exception($"Failed to find item with id: {request.ItemId}");
			}

			// NOTE: I would like to not have to do this...
			// potentially have an extension or helper method
			// that would add the updatable for us if null
			// -- nexus4880, 2024-10-24
			var upd = item.upd ??= new ItemUpdatable();
			var foldable = upd.Foldable ??= new ItemFoldableComponent();
			foldable.Folded = request.Value;

			return Task.CompletedTask;
		}
	}
}