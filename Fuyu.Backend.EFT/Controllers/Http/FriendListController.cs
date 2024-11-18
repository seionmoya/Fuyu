using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class FriendListController : HttpController
    {
        public FriendListController() : base("/client/friend/list")
        {
        }

        public override Task RunAsync(HttpContext context)
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

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}