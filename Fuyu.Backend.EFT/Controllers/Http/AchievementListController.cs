using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AchievementListController : HttpController
    {
        public AchievementListController() : base("/client/achievement/list")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetAchievementList());
        }
    }
}