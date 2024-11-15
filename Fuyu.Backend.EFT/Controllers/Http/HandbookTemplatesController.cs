using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HandbookTemplatesController : HttpController
    {
        public HandbookTemplatesController() : base("/client/handbook/templates")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetHandbook());
        }
    }
}