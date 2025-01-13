using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.Core.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers;

public class StoreGameListController : AbstractCoreHttpController
{
    public StoreGameListController() : base("/store/game/list")
    {
    }

    public override Task RunAsync(CoreHttpContext context)
    {
        var response = new Dictionary<string, string>()
        {
            { "eft", "http://localhost:8010" },
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text);
    }
}