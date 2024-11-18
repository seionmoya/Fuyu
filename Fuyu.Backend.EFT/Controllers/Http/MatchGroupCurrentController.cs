using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class MatchGroupCurrentController : HttpController
    {
        public MatchGroupCurrentController() : base("/client/match/group/current")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<MatchGroupCurrentResponse>()
            {
                data = new MatchGroupCurrentResponse()
                {
                    squad = [],
                    raidSettings = null
                }
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}