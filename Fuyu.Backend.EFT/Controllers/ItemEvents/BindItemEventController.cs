using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

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
