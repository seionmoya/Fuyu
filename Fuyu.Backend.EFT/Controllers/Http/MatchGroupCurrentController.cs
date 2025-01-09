using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class MatchGroupCurrentController : AbstractEftHttpController
{
    public MatchGroupCurrentController() : base("/client/match/group/current")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18
        var response = new ResponseBody<MatchGroupCurrentResponse>()
        {
            data = new MatchGroupCurrentResponse()
            {
                squad = [],
                raidSettings = null
            }
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}