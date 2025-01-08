using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;
using System.Threading.Tasks;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountLoginController : CoreHttpController<AccountLoginRequest>
    {
        private readonly AccountService _accountService;

        public AccountLoginController() : base("/account/login")
        {
            _accountService = AccountService.Instance;
        }

        public override Task RunAsync(CoreHttpContext context, AccountLoginRequest body)
        {
            var response = _accountService.LoginAccount(body.Username, body.Password);

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}