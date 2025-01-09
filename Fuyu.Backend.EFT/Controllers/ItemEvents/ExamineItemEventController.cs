using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents;

public class ExamineItemEventController : AbstractItemEventController<ExamineItemEvent>
{
    private readonly EftOrm _eftOrm;

    public ExamineItemEventController() : base("Examine")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(ItemEventContext context, ExamineItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);

        profile.Pmc.Encyclopedia[request.TemplateId] = true;

        return Task.CompletedTask;
    }
}