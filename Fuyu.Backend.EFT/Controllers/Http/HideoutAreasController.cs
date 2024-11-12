using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutAreasController : HttpController
    {
        public HideoutAreasController() : base("/client/hideout/areas")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetHideoutAreas());
        }
    }
}