using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class AddNoteItemEventController : ItemEventController<AddNoteItemEvent>
    {
        public AddNoteItemEventController() : base("AddNote")
        {
        }

        public override Task RunAsync(ItemEventContext context, AddNoteItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);

            profile.Pmc.Notes.Notes.Add(request.Note);

            return Task.CompletedTask;
        }
    }
}
