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
        private readonly TraderDatabase _traderDatabase;

        public TraderSettingsController() : base("/client/trading/api/traderSettings")
        {
            _traderDatabase = TraderDatabase.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var response = new ResponseBody<IEnumerable<TraderTemplate>>
            {
                data = _traderDatabase.GetTraderTemplates().Values
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}