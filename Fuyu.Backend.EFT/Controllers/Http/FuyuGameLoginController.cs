using System.Threading.Tasks;
using Fuyu.Backend.EFT.Services;
using Fuyu.Backend.Common.Models.Requests;
using Fuyu.Backend.Common.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class FuyuGameLoginController : HttpController<FuyuGameLoginRequest>
    {
        public FuyuGameLoginController() : base("/fuyu/game/login")
        {
        }

        public override Task RunAsync(HttpContext context, FuyuGameLoginRequest request)
        {
            var sessionId = AccountService.LoginAccount(request.AccountId);
            var response = new FuyuGameLoginResponse()
            {
                SessionId = sessionId
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}