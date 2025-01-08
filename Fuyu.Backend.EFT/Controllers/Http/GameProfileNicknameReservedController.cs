using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileNicknameReservedController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public GameProfileNicknameReservedController() : base("/client/game/profile/nickname/reserved")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var sessionId = context.GetSessionId();
            var account = _eftOrm.GetAccount(sessionId);

            var response = new ResponseBody<string>()
            {
                data = account.Username
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}