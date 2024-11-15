using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameLogoutController : HttpController
    {
        public GameLogoutController() : base("/client/game/logout")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<GameLogoutResponse>()
            {
                data = new GameLogoutResponse()
                {
                    status = "ok"
                }
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}