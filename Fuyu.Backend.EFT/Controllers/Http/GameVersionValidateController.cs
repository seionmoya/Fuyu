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

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<object>()
            {
                data = null
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}