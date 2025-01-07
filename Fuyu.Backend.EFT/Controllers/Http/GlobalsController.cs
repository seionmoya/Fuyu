using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class GlobalsController : EftHttpController
    {
        public GlobalsController() : base("/client/globals")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetGlobals();
            return context.SendJsonAsync(text, true, true);
        }
    }
}