using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class AchievementListController : EftHttpController
    {
        public AchievementListController() : base("/client/achievement/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetAchievementList();
            return context.SendJsonAsync(text, true, true);
        }
    }
}