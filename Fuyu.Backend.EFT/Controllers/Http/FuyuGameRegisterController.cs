using System.Threading.Tasks;
using Fuyu.Backend.Common.Models.Requests;
using Fuyu.Backend.Common.Models.Responses;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class FuyuGameRegisterController : HttpController<FuyuGameRegisterRequest>
    {
        public FuyuGameRegisterController() : base("/fuyu/game/register")
        {
        }

        public override Task RunAsync(HttpContext context, FuyuGameRegisterRequest request)
        {
            var accountId = AccountService.RegisterAccount(request.Username, request.Edition);
            var response = new FuyuGameRegisterResponse()
            {
                AccountId = accountId
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}