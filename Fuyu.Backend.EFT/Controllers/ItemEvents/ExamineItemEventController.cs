using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
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
