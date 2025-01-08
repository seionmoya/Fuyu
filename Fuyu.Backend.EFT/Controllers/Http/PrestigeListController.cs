using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class PrestigeListController : AbstractEftHttpController
    {
        public PrestigeListController() : base("/client/prestige/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2025-01-04
            var text = EftOrm.Instance.GetPrestige();
            return context.SendJsonAsync(text, true, true);
        }
    }
}