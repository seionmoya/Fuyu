using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class TraderSettingsController : HttpController
    {
        public TraderSettingsController() : base("/client/trading/api/traderSettings")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<IEnumerable<TraderTemplate>>
            {
                data = TraderDatabase.GetTraderTemplates().Values
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}