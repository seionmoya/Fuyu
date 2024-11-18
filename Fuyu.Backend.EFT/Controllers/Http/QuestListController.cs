using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class QuestListController : HttpController
    {
        public QuestListController() : base("/client/quest/list")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetQuest());
        }
    }
}