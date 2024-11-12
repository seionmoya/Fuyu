using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountRegisterGameController : HttpController<AccountRegisterGameRequest>
	{
        public AccountRegisterGameController() : base("/account/register/game")
        {
        }

        public override Task RunAsync(HttpContext context, AccountRegisterGameRequest request)
        {
            var sessionId = context.GetSessionId();
            var result = AccountService.RegisterGame(sessionId, request.Game, request.Edition);

            return context.SendJsonAsync(Json.Stringify(result));
        }
    }
}