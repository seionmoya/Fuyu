using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Servers;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class ServerListController : HttpController
    {
        public ServerListController() : base("/client/server/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
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

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}