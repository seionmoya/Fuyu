using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutAreasController : AbstractEftHttpController
    {
        public HideoutAreasController() : base("/client/hideout/areas")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetHideoutAreas();
            return context.SendJsonAsync(text, true, true);
        }
    }
}