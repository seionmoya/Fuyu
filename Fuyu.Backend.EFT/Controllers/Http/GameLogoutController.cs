using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class GameLogoutController : AbstractEftHttpController
{
    public GameLogoutController() : base("/client/game/logout")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18
        var response = new ResponseBody<GameLogoutResponse>()
        {
            data = new GameLogoutResponse()
            {
                status = "ok"
            }
        };

        return context.SendResponseAsync(response, true, true);
    }
}