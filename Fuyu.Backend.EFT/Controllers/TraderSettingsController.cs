using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Trading;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers
{
    public class TraderSettingsController : HttpController
    {
        public TraderSettingsController() : base("/client/trading/api/traderSettings")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<IEnumerable<TraderTemplate>>
            {
                data = TraderDatabase.GetTraderTemplates().Values
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}