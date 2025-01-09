using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers;

public class AccountRegisterController : CoreHttpController<AccountRegisterRequest>
{
    private readonly AccountService _accountService;

    public AccountRegisterController() : base("/account/register")
    {
        _accountService = AccountService.Instance;
    }

    public override Task RunAsync(CoreHttpContext context, AccountRegisterRequest request)
    {
        var result = _accountService.RegisterAccount(request.Username, request.Password);
        var response = new AccountRegisterResponse()
        {
            Status = result
        };

        return context.SendJsonAsync(Json.Stringify(response));
    }
}