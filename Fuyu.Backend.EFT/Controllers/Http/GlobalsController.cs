using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GlobalsController : HttpController
    {
        public GlobalsController() : base("/client/globals")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetGlobals());
        }
    }
}