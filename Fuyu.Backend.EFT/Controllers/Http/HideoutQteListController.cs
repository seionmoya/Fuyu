using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutQteListController : HttpController
    {
        public HideoutQteListController() : base("/client/hideout/qte/list")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetHideoutQteList());
        }
    }
}