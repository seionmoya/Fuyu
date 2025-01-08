using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers
{
    public class AccountGamesController : AbstractCoreHttpController
    {
        public AccountGamesController() : base("/account/games")
        {
        }

        public override Task RunAsync(CoreHttpContext context)
        {
            var sessionId = context.GetSessionId();
            var result = AccountService.Instance.GetGames(sessionId);
            var response = new AccountGamesResponse()
            {
                Games = result
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}
