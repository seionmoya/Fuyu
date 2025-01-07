using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GameProfileListController : EftHttpController
    {
        public GameProfileListController() : base("/client/game/profile/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var sessionId = context.GetSessionId();
            var profile = EftOrm.Instance.GetActiveProfile(sessionId);
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

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}