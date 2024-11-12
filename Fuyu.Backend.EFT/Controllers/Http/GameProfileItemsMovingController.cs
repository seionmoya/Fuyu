using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.Networking;
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
		public ItemEventRouter ItemEventRouter { get; } = new ItemEventRouter();

		public GameProfileItemsMovingController() : base("/client/game/profile/items/moving")
		{
			ItemEventRouter.AddController<CustomizationBuyEventController>();
			ItemEventRouter.AddController<EatItemEventController>();
			ItemEventRouter.AddController<InsureEventController>();
			ItemEventRouter.AddController<InterGameTransferEventController>();
			ItemEventRouter.AddController<MoveItemEventController>();
			ItemEventRouter.AddController<ReadEncyclopediaEventController>();
			ItemEventRouter.AddController<SellAllFromSavageEventController>();
			ItemEventRouter.AddController<TraderRepairEventController>();
			ItemEventRouter.AddController<TradingConfirmEventController>();
			ItemEventRouter.AddController<ApplyInventoryChangesItemEventController>();
			ItemEventRouter.AddController<RemoveItemEventController>();
			ItemEventRouter.AddController<FoldItemEventController>();
			ItemEventRouter.AddController<BindItemEventController>();
			ItemEventRouter.AddController<UnbindItemEventController>();
			ItemEventRouter.AddController<AddToWishListItemEventController>();
			ItemEventRouter.AddController<RemoveFromWishListItemEventController>();
			ItemEventRouter.AddController<ChangeWishlistItemCategoryItemEventController>();
			ItemEventRouter.AddController<AddNoteItemEventController>();
			ItemEventRouter.AddController<EditNoteItemEventController>();
			ItemEventRouter.AddController<DeleteNoteItemEventController>();
			ItemEventRouter.AddController<ExamineItemEventController>();
			ItemEventRouter.AddController<RecodeItemEventController>();
			ItemEventRouter.AddController<TagItemEventController>();
			ItemEventRouter.AddController<ToggleItemEventController>();
		}

		public override async Task RunAsync(HttpContext context, JObject request)
		{
			if (!request.ContainsKey("data"))
			{
				return;
			}

			var sessionId = context.GetSessionId();
			var profile = EftOrm.GetActiveProfile(sessionId);
			var requestData = request.Value<JArray>("data");
			var itemEventResponse = new ItemEventResponse();
			/*{
				
				ProfileChanges = {
					// NOTE: Possibly make this a method where we can do
					// context.GetProfileChange() and if it doesn't exist add it
					// -- nexus4880, 2024-10-22
					{ profile.Pmc._id, new ProfileChange() },
					{ profile.Savage._id, new ProfileChange() }
				},
				InventoryWarnings = []
				
			};*/

			itemEventResponse.ProfileChanges[profile.Pmc._id] = new ProfileChange();
			itemEventResponse.ProfileChanges[profile.Savage._id] = new ProfileChange();

			var requestIndex = 0;
			foreach (var itemRequest in requestData)
			{
				var action = itemRequest.Value<string>("Action");
				var itemEventContext = new ItemEventContext(sessionId, action, requestIndex, itemRequest, itemEventResponse);
				await ItemEventRouter.RouteAsync(itemEventContext);
				requestIndex++;
			}

			var response = new ResponseBody<ItemEventResponse>
			{
				data = itemEventResponse
			};

			await context.SendJsonAsync(Json.Stringify(response));
		}
	}
}
