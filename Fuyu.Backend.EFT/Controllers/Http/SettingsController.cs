using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class SettingsController : EftHttpController
    {
        public SettingsController() : base("/client/settings")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.GetSettings();
            return context.SendJsonAsync(text, true, true);
        }
    }
}