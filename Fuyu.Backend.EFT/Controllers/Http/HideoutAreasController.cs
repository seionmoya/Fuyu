using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutAreasController : HttpController
    {
        public HideoutAreasController() : base("/client/hideout/areas")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetHideoutAreas());
        }
    }
}