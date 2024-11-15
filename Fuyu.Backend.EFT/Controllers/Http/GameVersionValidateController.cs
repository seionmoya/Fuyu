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
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameVersionValidateController : HttpController
    {
        public GameVersionValidateController() : base("/client/game/version/validate")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<object>()
            {
                data = null
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}