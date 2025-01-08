using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Hashing;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class NotifierChannelCreateController : AbstractEftHttpController
    {
        public NotifierChannelCreateController() : base("/client/notifier/channel/create")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var channelId = SimpleId.Generate(64);

            // TODO: don't hardcode address
            // --seionmoya, 2024-11-18
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

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}