using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
	public class BindItemEventController : ItemEventController<BindItemEvent>
	{
		public BindItemEventController() : base("Bind")
		{
		}

		public override Task RunAsync(ItemEventContext context, BindItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);

			profile.Pmc.Inventory.FastPanel[request.Index] = request.Item;

			return Task.CompletedTask;
		}
	}
}
