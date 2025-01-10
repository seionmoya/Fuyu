using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class BuildsListController : AbstractEftHttpController
{
    public BuildsListController() : base("/client/builds/list")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var sessionId = context.SessionId;
        var account = EftOrm.Instance.GetAccount(sessionId);
        var profile = EftOrm.Instance.GetActiveProfile(account);
        var builds = profile.Builds;

        var response = new ResponseBody<BuildsListResponse>
        {
            data = builds
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}