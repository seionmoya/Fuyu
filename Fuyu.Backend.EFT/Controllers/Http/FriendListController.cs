using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class FriendListController : HttpController
    {
        public FriendListController() : base("/client/friend/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<FriendListResponse>()
            {
                data = new FriendListResponse()
                {
                    Friends = [],
                    Ignore = [],
                    InIgnoreList = []
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}