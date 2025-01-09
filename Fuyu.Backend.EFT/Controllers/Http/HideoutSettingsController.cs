using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutSettingsController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HideoutSettingsController() : base("/client/hideout/settings")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var settings = _eftOrm.GetHideoutSettings();
            var response = new ResponseBody<HideoutSettingsResponse>()
            {
                data = settings
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}