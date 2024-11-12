using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class ExamineItemEventController : ItemEventController<ExamineItemEvent>
    {
        public ExamineItemEventController() : base("Examine")
        {
        }

        public override Task RunAsync(ItemEventContext context, ExamineItemEvent request)
        {
            var profile = EftOrm.GetActiveProfile(context.SessionId);

            profile.Pmc.Encyclopedia[request.TemplateId] = true;

            return Task.CompletedTask;
        }
    }
}
