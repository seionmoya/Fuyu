using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class GameProfileNicknameChangeController : AbstractEftHttpController<GameProfileNicknameChangeRequest>
{
    private readonly ProfileService _profileService;
    private readonly EftOrm _eftOrm;

    public GameProfileNicknameChangeController() : base("/client/game/profile/nickname/change")
    {
        _profileService = ProfileService.Instance;
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context, GameProfileNicknameChangeRequest request)
    {
        // TODO:
        // * validate nickname usage
        // -- seionmoya, 2024/08/28

        var result = _profileService.IsValidNickname(request.Nickname);
        // TODO: Find if there is a more proper usage of EBackendErrorCode for this switch (find actual error message in globals.json)
        var errorMessage = result switch
        {
            ENicknameChangeResult.WrongSymbol => EBackendErrorCode.NicknameNotValid,
            ENicknameChangeResult.TooShort => EBackendErrorCode.NicknameNotValid,
            ENicknameChangeResult.CharacterLimit => EBackendErrorCode.NicknameNotValid,
            ENicknameChangeResult.InvalidNickname => EBackendErrorCode.NicknameNotValid,
            ENicknameChangeResult.NicknameTaken => EBackendErrorCode.NicknameNotUnique,
            ENicknameChangeResult.NicknameChangeTimeout => EBackendErrorCode.NicknameChangeTimeout,
            ENicknameChangeResult.DigitsLimit => EBackendErrorCode.NicknameNotValid,
            ENicknameChangeResult.Ok => EBackendErrorCode.None,
            _ => EBackendErrorCode.None,
        };

        if (result == ENicknameChangeResult.Ok)
        {
            //TODO: Save profile
            var profile = _eftOrm.GetProfile(context.SessionId);
            profile.Pmc.Info.Nickname = request.Nickname;
        }

        var response = new ResponseBody<GameProfileNicknameChangeResponse>()
        {
            err = (int)errorMessage,
            errmsg = errorMessage.ToString(),
            data = new GameProfileNicknameChangeResponse()
            {
                Status = result
            }
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}