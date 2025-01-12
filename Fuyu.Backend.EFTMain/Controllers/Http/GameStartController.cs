using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFTMain.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class GameStartController : AbstractEftHttpController
{
    public GameStartController() : base("/client/game/start")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: generate this
        // --seionmoya, 2024-11-18
        var response = new ResponseBody<GameStartResponse>()
        {
            data = new GameStartResponse()
            {
                utc_time = 1711579783.2164
            }
        };

        return context.SendResponseAsync(response, true, true);
    }
}