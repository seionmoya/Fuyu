using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutSettingsController : AbstractEftHttpController
    {
        private readonly HideoutService _hideoutService;

        public HideoutSettingsController() : base("/client/hideout/settings")
        {
            _hideoutService = HideoutService.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var settings = _hideoutService.GetSettings();
            var response = new ResponseBody<HideoutSettingsResponse>()
            {
                data = settings
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}