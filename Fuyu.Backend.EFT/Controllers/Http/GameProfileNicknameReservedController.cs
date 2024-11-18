using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileNicknameReservedController : HttpController
    {
        public GameProfileNicknameReservedController() : base("/client/game/profile/nickname/reserved")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            var account = EftOrm.GetAccount(sessionId);

            var response = new ResponseBody<string>()
            {
                data = account.Username
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}