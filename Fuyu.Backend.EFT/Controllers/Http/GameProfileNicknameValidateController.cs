using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileNicknameValidateController : EftHttpController<GameProfileNicknameValidateRequest>
    {
        public GameProfileNicknameValidateController() : base("/client/game/profile/nickname/validate")
        {
        }

        public override Task RunAsync(EftHttpContext context, GameProfileNicknameValidateRequest request)
        {
            // TODO:
            // * validate nickname usage
            // -- seionmoya, 2024/08/28

            var response = new ResponseBody<GameProfileNicknameValidateResponse>()
            {
                data = new GameProfileNicknameValidateResponse()
                {
                    status = "ok"
                }
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}