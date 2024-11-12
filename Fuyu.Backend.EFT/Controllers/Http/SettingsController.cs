using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class SettingsController : HttpController
    {
        public SettingsController() : base("/client/settings")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetSettings());
        }
    }
}