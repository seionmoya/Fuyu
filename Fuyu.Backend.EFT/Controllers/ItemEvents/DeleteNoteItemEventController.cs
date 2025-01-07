using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class DeleteNoteItemEventController : ItemEventController<DeleteNoteItemEvent>
    {
        public DeleteNoteItemEventController() : base("DeleteNote")
        {
        }

        public override Task RunAsync(ItemEventContext context, DeleteNoteItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var notes = profile.Pmc.Notes.Notes;

            if (request.Index < 0 || request.Index > notes.Count)
            {
                context.AppendInventoryError($"Notes index {request.Index} outside bounds of array");

                return Task.CompletedTask;
            }

            notes.RemoveAt(request.Index);

            return Task.CompletedTask;
        }
    }
}
