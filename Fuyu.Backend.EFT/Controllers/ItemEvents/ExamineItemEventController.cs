using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class ExamineItemEventController : AbstractItemEventController<ExamineItemEvent>
    {
        public ExamineItemEventController() : base("Examine")
        {
        }

        public override Task RunAsync(ItemEventContext context, ExamineItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);

            profile.Pmc.Encyclopedia[request.TemplateId] = true;

            return Task.CompletedTask;
        }
    }
}
