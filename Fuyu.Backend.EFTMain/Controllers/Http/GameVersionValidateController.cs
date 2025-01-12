/* TODO: GameVersionValidateRequest
{
    "version": {
        "major": "0.15.5.1.33420",
        "minor": "live",
        "game": "live",
        "backend": "6",
        "taxonomy": "341"
    },
    "develop": true
}
*/

using System.Threading.Tasks;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Networking;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class GameVersionValidateController : AbstractEftHttpController
{
    private readonly ResponseService _responseService;

    public GameVersionValidateController() : base("/client/game/version/validate")
    {
        _responseService = ResponseService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18

        return context.SendJsonAsync(_responseService.EmptyJsonResponse, true, true);
    }
}