using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileNicknameReservedController : EftHttpController
    {
        public GameProfileNicknameReservedController() : base("/client/game/profile/nickname/reserved")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var sessionId = context.GetSessionId();
            var account = EftOrm.Instance.GetAccount(sessionId);

            var response = new ResponseBody<string>()
            {
                data = account.Username
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}