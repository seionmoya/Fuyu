using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.EFT.Services;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameBotGenerateController : HttpController<GameBotGenerateRequest>
    {
        public GameBotGenerateController() : base("/client/game/bot/generate")
        {
        }

        public override Task RunAsync(HttpContext context, GameBotGenerateRequest request)
        {
            var profiles = BotService.GetBots(request.conditions);
            var response = new ResponseBody<Profile[]>()
            {
                data = profiles
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}