using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameStartController : HttpController
    {
        public GameStartController() : base("/client/game/start")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<GameStartResponse>()
            {
                data = new GameStartResponse()
                {
                    utc_time = 1711579783.2164
                }
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}