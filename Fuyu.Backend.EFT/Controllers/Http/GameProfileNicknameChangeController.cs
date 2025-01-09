using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class GameProfileNicknameChangeController : AbstractEftHttpController<GameProfileNicknameChangeRequest>
{
    private readonly ProfileService _profileService;

    public GameProfileNicknameChangeController() : base("/client/game/profile/nickname/validate")
    {
        _profileService = ProfileService.Instance;
    }

    public override Task RunAsync(EftHttpContext context, GameProfileNicknameChangeRequest request)
    {
        // TODO:
        // * validate nickname usage
        // -- seionmoya, 2024/08/28

        bool validNickname = _profileService.IsValidNickname(request.Nickname, out string status);        

        if (validNickname)
        {
            //TODO: Save profile
        }

        var response = new ResponseBody<GameProfileNicknameChangeResponse>()
        {
            data = new GameProfileNicknameChangeResponse()
            {
                Status = status
            }
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}