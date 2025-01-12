using System.Threading.Tasks;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Networking;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class FriendRequestListInboxController : AbstractEftHttpController
{
    private readonly ResponseService _responseService;

    public FriendRequestListInboxController() : base("/client/friend/request/list/inbox")
    {
        _responseService = ResponseService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: generate this
        // --seionmoya, 2024-11-18

        return context.SendJsonAsync(_responseService.EmptyJsonArrayResponse, true, true);
    }
}