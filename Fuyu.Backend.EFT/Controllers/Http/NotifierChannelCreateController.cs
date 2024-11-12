using System.Threading.Tasks;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;

namespace Fuyu.Backend.EFT.Controllers
{
    public class NotifierChannelCreateController : HttpController
    {
        public NotifierChannelCreateController() : base("/client/notifier/channel/create")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var channelId = SimpleId.Generate(64);
            var response = new ResponseBody<NotifierChannelCreateResponse>
            {
                data = new NotifierChannelCreateResponse()
                {
                    Server = "localhost:8010",
                    ChannelId = channelId,
                    URL = $"http://localhost:8010/push/notifier/get/{channelId}",
                    WS = $"ws://localhost:8010/push/notifier/getwebsocket/{channelId}"
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}