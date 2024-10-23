using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Backend.EFT.ItemEvents.Controllers;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers
{
	public class GameProfileItemsMovingController : HttpController<JObject>
	{
		private ItemEventRouter _router = new ItemEventRouter();

		public GameProfileItemsMovingController() : base("/client/game/profile/items/moving")
		{
			_router.AddController<CustomizationBuyEventController>();
			_router.AddController<EatItemEventController>();
			_router.AddController<InsureEventController>();
			_router.AddController<InterGameTransferEventController>();
			_router.AddController<MoveItemEventController>();
			_router.AddController<ReadEncyclopediaEventController>();
			_router.AddController<SellAllFromSavageEventController>();
			_router.AddController<TraderRepairEventController>();
			_router.AddController<TradingConfirmEventController>();
			_router.AddController<ApplyInventoryChangesItemEventController>();
		}

		public override async Task RunAsync(HttpContext context, JObject request)
		{
			if (!request.ContainsKey("data"))
			{
				return;
			}

			var sessionId = context.GetSessionId();
			var account = EftOrm.GetAccount(sessionId);
			var profile = EftOrm.GetProfile(account.PveId);
			var requestData = request.Value<JArray>("data");
			var response = new ItemEventResponse
			{
				ProfileChanges = {
					// NOTE: Possibly make this a method where we can do
					// context.GetProfileChange() and if it doesn't exist add it
					// -- nexus4880, 2024-10-22
					{ profile.Pmc._id, new ProfileChange() },
					{ profile.Savage._id, new ProfileChange() }
				},
				InventoryWarnings = []
			};

			int requestIndex = 0;
			foreach (var itemRequest in requestData)
			{
				var action = itemRequest.Value<string>("Action");
				var itemEventContext = new ItemEventContext(sessionId, action, requestIndex, itemRequest, response);
				await _router.RouteAsync(itemEventContext);
				requestIndex++;
			}

			await context.SendJsonAsync(Json.Stringify(new ResponseBody<ItemEventResponse>
			{
				data = response
			}));
		}
	}
}
