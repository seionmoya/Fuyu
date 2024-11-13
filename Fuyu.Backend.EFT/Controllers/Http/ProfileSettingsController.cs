using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class ProfileSettingsController : HttpController
    {
        public ProfileSettingsController() : base("/client/profile/settings")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<bool>()
            {
                data = true
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}