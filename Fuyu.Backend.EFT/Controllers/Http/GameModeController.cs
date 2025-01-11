using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class GameModeController : AbstractEftHttpController<ClientGameModeRequest>
{
    private readonly EftOrm _eftOrm;

    public GameModeController() : base("/client/game/mode")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context, ClientGameModeRequest body)
    {
        var account = _eftOrm.GetAccount(context.SessionId);

        if (body.SessionMode == null)
        {
            // wiped profile
            body.SessionMode = ESessionMode.Pve;
        }

        account.CurrentSession = body.SessionMode;

        var response = new ResponseBody<GameModeResponse>()
        {
            // TODO: don't use hardcoded address
            // --seionmoya, 2024-11-18
            data = new GameModeResponse()
            {
                GameMode = body.SessionMode,
                BackendUrl = "http://localhost:8010"
            }
        };

        return context.SendResponseAsync(response, true, true);
    }
}