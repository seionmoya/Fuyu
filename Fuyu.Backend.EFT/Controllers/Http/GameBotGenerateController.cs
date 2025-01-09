using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameBotGenerateController : EftHttpController<GameBotGenerateRequest>
    {
        private readonly BotService _botService;

        public GameBotGenerateController() : base("/client/game/bot/generate")
        {
            _botService = BotService.Instance;
        }

        public override Task RunAsync(EftHttpContext context, GameBotGenerateRequest request)
        {
            var profiles = _botService.GetBots(request.conditions);
            var response = new ResponseBody<Profile[]>()
            {
                data = profiles
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}