using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class AchievementStatisticController : AbstractEftHttpController
{
    private readonly AchievementService _achievementService;

    public AchievementStatisticController() : base("/client/achievement/statistic")
    {
        _achievementService = AchievementService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var statistics = _achievementService.GetStatistics();
        var response = new ResponseBody<AchievementStatisticResponse>()
        {
            data = statistics
        };

        return context.SendResponseAsync(response, true, true);
    }
}