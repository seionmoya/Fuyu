using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
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

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}