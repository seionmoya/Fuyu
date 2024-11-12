using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AccountCustomizationController : HttpController
    {
        public AccountCustomizationController() : base("/client/account/customization")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetAccountCustomization());
        }
    }
}