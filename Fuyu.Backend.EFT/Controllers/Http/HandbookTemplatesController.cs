using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HandbookTemplatesController : AbstractEftHttpController
    {
        public HandbookTemplatesController() : base("/client/handbook/templates")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetHandbook();
            return context.SendJsonAsync(text, true, true);
        }
    }
}