using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HandbookTemplatesController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HandbookTemplatesController() : base("/client/handbook/templates")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var response = _eftOrm.GetHandbook();
            var text = response.ToString();
            return context.SendJsonAsync(text, true, true);
        }
    }
}