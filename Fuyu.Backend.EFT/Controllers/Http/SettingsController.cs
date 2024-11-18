using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class SettingsController : HttpController
    {
        public SettingsController() : base("/client/settings")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetSettings());
        }
    }
}