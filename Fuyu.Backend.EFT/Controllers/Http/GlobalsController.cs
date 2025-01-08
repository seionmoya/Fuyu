using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GlobalsController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public GlobalsController() : base("/client/globals")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = _eftOrm.GetGlobals();
            return context.SendJsonAsync(text, true, true);
        }
    }
}