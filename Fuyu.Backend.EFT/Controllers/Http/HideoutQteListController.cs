using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutQteListController : EftHttpController
    {
        public HideoutQteListController() : base("/client/hideout/qte/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.GetHideoutQteList();
            return context.SendJsonAsync(text, true, true);
        }
    }
}