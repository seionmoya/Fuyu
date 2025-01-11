using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Servers;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class GameConfigController : AbstractEftHttpController
{
    public GameConfigController() : base("/client/game/config")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var response = new ResponseBody<GameConfigResponse>
        {
            data = new GameConfigResponse()
            {
                // TODO: don't use hardcoded path
                // --seionmoya, 2024-11-18
                backend = new Backends()
                {
                    Lobby = "http://localhost:8010",
                    Trading = "http://localhost:8010",
                    Messaging = "http://localhost:8010",
                    Main = "http://localhost:8010",
                    RagFair = "http://localhost:8010"
                },
                // TODO: generate this
                // --seionmoya, 2024-11-18
                utc_time = 1724450891.010541,
                reportAvailable = true,
                // TODO: handle this
                // --seionmoya, 2024-11-18
                purchasedGames = new PurchasedGames()
                {
                    eft = true,
                    arena = true
                },
                // TODO: handle this
                // --seionmoya, 2024-11-18
                isGameSynced = true
            }
        };

        return context.SendResponseAsync(response, true, true);
    }
}