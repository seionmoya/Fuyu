using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutSettingsController : EftHttpController
    {
        public HideoutSettingsController() : base("/client/hideout/settings")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var json = EftOrm.Instance.GetHideoutSettings();
            var response = Json.Parse<ResponseBody<HideoutSettingsResponse>>(json);

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}