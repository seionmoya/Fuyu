using System.Threading.Tasks;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class AddNoteItemEventController : ItemEventController<AddNoteItemEvent>
	{
		public AddNoteItemEventController() : base("AddNote")
		{
		}

		public override Task RunAsync(ItemEventContext context, AddNoteItemEvent request)
		{
			var profile = EftOrm.GetActiveProfile(context.SessionId);

			profile.Pmc.Notes.Notes.Add(request.Note);

			return Task.CompletedTask;
		}
	}
}
