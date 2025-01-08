using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutCustomizationOfferListController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HideoutCustomizationOfferListController() : base("/client/hideout/customization/offer/list")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2025-01-04
            var text = _eftOrm.GetHideoutCustomizationOfferList();
            return context.SendJsonAsync(text, true, true);
        }
    }
}