using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileListController : HttpController
    {
        public GameProfileListController() : base("/client/game/profile/list")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            var profile = EftOrm.GetActiveProfile(sessionId);
            Profile[] profiles;

            if (profile.ShouldWipe)
            {
                profiles = [];
            }
            else
            {
                profiles = [profile.Pmc, profile.Savage];
            }

            var response = new ResponseBody<Profile[]>()
            {
                data = profiles
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}