using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AchievementStatisticController : HttpController
    {
        public AchievementStatisticController() : base("/client/achievement/statistic")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var json = EftOrm.GetAchievementStatistic();
            var response = Json.Parse<ResponseBody<AchievementStatisticResponse>>(json);
            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}