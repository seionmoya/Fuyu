using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class MailDialogListController : HttpController
    {
        public MailDialogListController() : base("/client/mail/dialog/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<object[]>
            {
                data = []
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}