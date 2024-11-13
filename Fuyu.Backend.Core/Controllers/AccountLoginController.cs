using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountLoginController : HttpController<AccountLoginRequest>
    {
        public AccountLoginController() : base("/account/login")
        {
        }

        public override Task RunAsync(HttpContext context, AccountLoginRequest body)
        {
            var response = AccountService.LoginAccount(body.Username, body.Password);

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}