using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutSettingsController : HttpController
    {
        public HideoutSettingsController() : base("/client/hideout/settings")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var json = EftOrm.GetHideoutSettings();
            var response = Json.Parse<ResponseBody<HideoutSettingsResponse>>(json);
            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}