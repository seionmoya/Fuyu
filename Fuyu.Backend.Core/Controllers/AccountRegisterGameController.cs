using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountRegisterGameController : CoreHttpController<AccountRegisterGameRequest>
    {
        public AccountRegisterGameController() : base("/account/register/game")
        {
        }

        public override Task RunAsync(CoreHttpContext context, AccountRegisterGameRequest request)
        {
            var sessionId = context.GetSessionId();
            var result = AccountService.Instance.RegisterGame(sessionId, request.Game, request.Edition);

            return context.SendJsonAsync(Json.Stringify(result));
        }
    }
}