using System.Threading.Tasks;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Networking;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class PutMetricsController : AbstractEftHttpController
{
    private readonly ResponseService _responseService;

    public PutMetricsController() : base("/client/putMetrics")
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