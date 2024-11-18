using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AccountCustomizationController : HttpController
    {
        public AccountCustomizationController() : base("/client/account/customization")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetAccountCustomization());
        }
    }
}