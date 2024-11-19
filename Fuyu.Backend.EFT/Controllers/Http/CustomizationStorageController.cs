using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationStorageController : EftHttpController
    {
        public CustomizationStorageController() : base("/client/trading/customization/storage")
        {
        }

        public override Task RunAsync(EftHttpContext context)
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

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}