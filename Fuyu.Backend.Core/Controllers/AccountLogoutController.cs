using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountLogoutController : HttpController
    {
        public AccountLogoutController() : base("/account/logout")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            CoreOrm.RemoveSession(sessionId);

            return context.SendJsonAsync("{}");
        }
    }
}
