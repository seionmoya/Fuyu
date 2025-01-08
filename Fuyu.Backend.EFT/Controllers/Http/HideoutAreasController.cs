using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutAreasController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HideoutAreasController() : base("/client/hideout/areas")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = _eftOrm.GetHideoutAreas();
            return context.SendJsonAsync(text, true, true);
        }
    }
}