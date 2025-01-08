using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class FriendRequestListInboxController : AbstractEftHttpController
    {
        public FriendRequestListInboxController() : base("/client/friend/request/list/inbox")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var response = new ResponseBody<object[]>()
            {
                data = []
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}