using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    // TODO:
    // * move code into TemplateTable and ProfileService
    // -- seionmoya, 2024/09/02
    public class GameProfileCreateController : EftHttpController<GameProfileCreateRequest>
    {
        public GameProfileCreateController() : base("/client/game/profile/create")
        {
        }

        public override Task RunAsync(EftHttpContext context, GameProfileCreateRequest request)
        {
            var sessionId = context.GetSessionId();
            var account = EftOrm.Instance.GetAccount(sessionId);
            var pmcId = ProfileService.Instance.WipeProfile(account, request.side, request.headId, request.voiceId);

            var response = new ResponseBody<GameProfileCreateResponse>()
            {
                data = new GameProfileCreateResponse()
                {
                    uid = pmcId
                }
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}