using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationStorageController : EftHttpController
    {
        public CustomizationStorageController() : base("/client/customization/storage")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var sessionId = context.GetSessionId();
            var profile = EftOrm.GetActiveProfile(sessionId);

            var response = new ResponseBody<CustomizationStorageEntry[]>()
            {
                data = profile.Customization
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}