using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AccountCustomizationController : EftHttpController
    {
        public AccountCustomizationController() : base("/client/account/customization")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.GetAccountCustomization();
            return context.SendJsonAsync(text, true, true);
        }
    }
}