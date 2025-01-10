using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class CheckVersionController : AbstractEftHttpController
{
    public CheckVersionController() : base("/client/checkVersion")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: Add global constant somewhere where we can define the supported version of EFT/Arena?
        // -- slejmur, 2025-01-09
        string currentVersion = "0.16.0.2.34510";
        var appVersion = context.EftVersion;
        appVersion = appVersion.Replace("EFT Client ", "");

        var response = new ResponseBody<CheckVersionResponse>()
        {
            data = new CheckVersionResponse()
            {
                isvalid = false,
                latestVersion = "0.16.0.2.34510"
            }
        };

        if (appVersion == currentVersion)
        {
            response.data.isvalid = true;
        }

        return context.SendResponseAsync(response, true, true);
    }
}