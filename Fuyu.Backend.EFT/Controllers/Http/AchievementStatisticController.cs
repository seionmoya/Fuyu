using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AchievementStatisticController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public AchievementStatisticController() : base("/client/achievement/statistic")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var json = _eftOrm.GetAchievementStatistic();
            var response = Json.Parse<ResponseBody<AchievementStatisticResponse>>(json);

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}