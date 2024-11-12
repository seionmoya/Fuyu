using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileNicknameReservedController : HttpController
    {
        public GameProfileNicknameReservedController() : base("/client/game/profile/nickname/reserved")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            var account = EftOrm.GetAccount(sessionId);

            var response = new ResponseBody<string>()
            {
                data = account.Username
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}