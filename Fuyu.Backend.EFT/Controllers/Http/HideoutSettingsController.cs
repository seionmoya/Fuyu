using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutSettingsController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HideoutSettingsController() : base("/client/hideout/settings")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var json = _eftOrm.GetHideoutSettings();
            var response = Json.Parse<ResponseBody<HideoutSettingsResponse>>(json);

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}