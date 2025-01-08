using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class EditNoteItemEventController : ItemEventController<EditNoteItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public EditNoteItemEventController() : base("EditNote")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, EditNoteItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var notes = profile.Pmc.Notes.Notes;

            if (request.Index < 0 || request.Index > notes.Count)
            {
                context.AppendInventoryError($"Notes index {request.Index} outside bounds of array");

                return Task.CompletedTask;
            }

            notes[request.Index] = request.Note;

            return Task.CompletedTask;
        }
    }
}
