using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
	public class AddNoteItemEventController : ItemEventController<AddNoteItemEvent>
	{
		public AddNoteItemEventController() : base("AddNote")
		{
		}

		public override Task RunAsync(ItemEventContext context, AddNoteItemEvent request)
		{
			var account = EftOrm.GetAccount(context.SessionId);
			var profile = EftOrm.GetProfile(account.PveId);

			profile.Pmc.Notes.Notes.Add(request.Note);

			return Task.CompletedTask;
		}
	}
}
