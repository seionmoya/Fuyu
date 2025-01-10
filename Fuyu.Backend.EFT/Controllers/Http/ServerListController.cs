using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Servers;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class ServerListController : AbstractEftHttpController
{
    public ServerListController() : base("/client/server/list")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var response = new ResponseBody<ServerInfo[]>()
        {
            data = [
                new ServerInfo
                {
                    ip = "127.0.0.1",
                    port = 8000
                }
            ]
        };

        return context.SendResponseAsync(response, true, true);
    }
}