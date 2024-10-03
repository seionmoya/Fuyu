﻿using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Responses;
using Fuyu.Backend.EFT.ItemEvents;
using Fuyu.Backend.EFT.ItemEvents.Controllers;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers
{
    public class ClientGameProfileItemsMoving : HttpController
    {
        private ItemEventRouter _router = new ItemEventRouter();

        public ClientGameProfileItemsMoving() : base("/client/game/profile/items/moving")
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
        }

        public override async Task RunAsync(HttpContext context)
        {
            var requestText = await context.GetTextAsync();
            var requestObject = JObject.Parse(requestText);
            var requestData = requestObject.Value<JArray>("data");
            Terminal.WriteLine($"requestData:{requestData.ToString(Formatting.None)}");
            var response = new ItemEventResponse
            {
                ProfileChanges = [],
                InventoryWarnings = []
            };

            var sessionId = context.GetSessionId();
            foreach (var itemRequest in requestData)
            {
                var action = itemRequest.Value<string>("Action");
                var itemEventContext = new ItemEventContext(sessionId, action, itemRequest, response);
                if (!await _router.RouteEvent(itemEventContext))
                {
                    Terminal.WriteLine($"Unhandled:{itemRequest.ToString(Formatting.None)}");
                }
            }

            await context.SendJsonAsync(Json.Stringify(new ResponseBody<ItemEventResponse>
            {
                data = response
            }));
        }
    }
}
