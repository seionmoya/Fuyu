using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class TraderSettingsController : EftHttpController
    {
        public TraderSettingsController() : base("/client/trading/api/traderSettings")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var response = new ResponseBody<IEnumerable<TraderTemplate>>
            {
                data = TraderDatabase.Instance.GetTraderTemplates().Values
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}