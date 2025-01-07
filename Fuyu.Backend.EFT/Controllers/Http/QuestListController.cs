using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class QuestListController : EftHttpController
    {
        public QuestListController() : base("/client/quest/list")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetQuests();
            return context.SendJsonAsync(text, true, true);
        }
    }
}