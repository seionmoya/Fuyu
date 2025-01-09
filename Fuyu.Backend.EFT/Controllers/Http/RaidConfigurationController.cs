using System.Threading.Tasks;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class RaidConfigurationController : AbstractEftHttpController
{
    private readonly ResponseService _responseService;

    public RaidConfigurationController() : base("/client/raid/configuration")
    {
        _responseService = ResponseService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: handle this
        // --seionmoya, 2024-11-18

        return context.SendJsonAsync(_responseService.EmptyJsonResponse, true, true);
    }
}