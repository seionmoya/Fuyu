using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFTMain.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class ProfileSettingsController : AbstractEftHttpController
{
    public ProfileSettingsController() : base("/client/profile/settings")
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18
        var response = new ResponseBody<bool>()
        {
            data = true
        };

        return context.SendResponseAsync(response, true, true);
    }
}