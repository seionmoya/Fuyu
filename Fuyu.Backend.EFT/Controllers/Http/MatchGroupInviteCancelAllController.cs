using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class MatchGroupInviteCancelAllController : HttpController
    {
        public MatchGroupInviteCancelAllController() : base("/client/match/group/invite/cancel-all")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<bool>()
            {
                data = true
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}