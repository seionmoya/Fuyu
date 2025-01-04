using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutCustomizationOfferListController : EftHttpController
    {
        public HideoutCustomizationOfferListController() : base("/client/hideout/customization/offer/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-01-04
            var text = EftOrm.GetHideoutCustomizationOfferList();
            return context.SendJsonAsync(text, true, true);
        }
    }
}