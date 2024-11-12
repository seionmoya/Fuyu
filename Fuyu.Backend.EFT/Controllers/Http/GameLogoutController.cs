using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameLogoutController : HttpController
    {
        public GameLogoutController() : base("/client/game/logout")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<GameLogoutResponse>()
            {
                data = new GameLogoutResponse()
                {
                    status = "ok"
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}