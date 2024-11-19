using System.Threading.Tasks;
using Fuyu.Backend.Core.Networking;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountLogoutController : CoreHttpController
    {
        public AccountLogoutController() : base("/account/logout")
        {
        }

        public override Task RunAsync(CoreHttpContext context)
        {
            var sessionId = context.GetSessionId();
            CoreOrm.RemoveSession(sessionId);

            return context.SendJsonAsync("{}");
        }
    }
}
