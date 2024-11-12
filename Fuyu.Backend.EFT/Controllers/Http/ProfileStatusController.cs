using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Multiplayer;
using Fuyu.Backend.EFT.DTO.Responses;

namespace Fuyu.Backend.EFT.Controllers
{
    public class ProfileStatusController : HttpController
    {
        public ProfileStatusController() : base("/client/profile/status")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            
            var profile = EftOrm.GetActiveProfile(sessionId);

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

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}