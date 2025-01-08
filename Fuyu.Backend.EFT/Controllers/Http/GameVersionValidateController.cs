/* TODO: GameVersionValidateRequest
{
    "version": {
        "major": "0.15.5.1.33420",
        "minor": "live",
        "game": "live",
        "backend": "6",
        "taxonomy": "341"
    },
    "develop": true
}
*/

using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameVersionValidateController : AbstractEftHttpController
    {
        public GameVersionValidateController() : base("/client/game/version/validate")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: handle this
            // --seionmoya, 2024-11-18
            var response = new ResponseBody<object>()
            {
                data = null
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}