using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Multiplayer;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class ProfileStatusController : EftHttpController
    {
        public ProfileStatusController() : base("/client/profile/status")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var sessionId = context.GetSessionId();

            var profile = EftOrm.Instance.GetActiveProfile(sessionId);

            // TODO: generate this
            // --seionmoya, 2024-11-18
            var response = new ResponseBody<ProfileStatusResponse>()
            {
                data = new ProfileStatusResponse()
                {
                    maxPveCountExceeded = false,
                    profiles =
                    [
                        new ProfileStatusInfo
                        {
                            profileid = profile.Pmc._id,
                            profileToken = null,
                            status = "Free",
                            sid = string.Empty,
                            ip = string.Empty,
                            port = 0
                        },
                        new ProfileStatusInfo
                        {
                            profileid = profile.Savage._id,
                            profileToken = null,
                            status = "Free",
                            sid = string.Empty,
                            ip = string.Empty,
                            port = 0
                        }
                    ]
                }
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}