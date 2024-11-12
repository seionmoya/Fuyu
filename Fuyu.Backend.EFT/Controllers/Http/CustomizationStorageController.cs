using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationStorageController : HttpController
    {
        public CustomizationStorageController() : base("/client/trading/customization/storage")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var sessionId = context.GetSessionId();
            var profile = EftOrm.GetActiveProfile(sessionId);

            var response = new ResponseBody<CustomizationStorageResponse>()
            {
                data = new CustomizationStorageResponse()
                {
                    _id = profile.Pmc._id,
                    suites = profile.Suites
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}