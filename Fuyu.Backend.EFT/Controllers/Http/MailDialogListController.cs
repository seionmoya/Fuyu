using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class MailDialogListController : AbstractEftHttpController
{
    public MailDialogListController() : base("/client/mail/dialog/list")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18
        var response = new ResponseBody<object[]>
        {
            data = []
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}