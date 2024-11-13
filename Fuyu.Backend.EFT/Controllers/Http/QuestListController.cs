using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class QuestListController : HttpController
    {
        public QuestListController() : base("/client/quest/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetQuest());
        }
    }
}