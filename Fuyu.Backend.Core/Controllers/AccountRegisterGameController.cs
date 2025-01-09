using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers;

public class AccountRegisterGameController : CoreHttpController<AccountRegisterGameRequest>
{
    private readonly AccountService _accountService;

    public AccountRegisterGameController() : base("/account/register/game")
    {
        _accountService = AccountService.Instance;
    }

    public override Task RunAsync(CoreHttpContext context, AccountRegisterGameRequest request)
    {
        var sessionId = context.SessionId;
        var result = _accountService.RegisterGame(sessionId, request.Game, request.Edition);

        return context.SendJsonAsync(Json.Stringify(result));
    }
}