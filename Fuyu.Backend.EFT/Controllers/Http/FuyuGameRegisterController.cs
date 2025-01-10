using System.Threading.Tasks;
using Fuyu.Backend.Common.Models.Requests;
using Fuyu.Backend.Common.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class FuyuGameRegisterController : AbstractEftHttpController<FuyuGameRegisterRequest>
{
    private readonly AccountService _accountService;

    public FuyuGameRegisterController() : base("/fuyu/game/register")
    {
        _accountService = AccountService.Instance;
    }

    public override Task RunAsync(EftHttpContext context, FuyuGameRegisterRequest request)
    {
        var accountId = _accountService.RegisterAccount(request.Username, request.Edition);
        var response = new FuyuGameRegisterResponse()
        {
            AccountId = accountId
        };

        var text = Json.Stringify(response);
        // NOTE: no need for encryption, request runs internal
        // -- seionmoya, 2024-11-18
        return context.SendJsonAsync(text, false, false);
    }
}