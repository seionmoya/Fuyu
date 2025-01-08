﻿using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;
using System.Threading.Tasks;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class AddNoteItemEventController : AbstractItemEventController<AddNoteItemEvent>
    {
        private readonly EftOrm _eftOrm;

        public AddNoteItemEventController() : base("AddNote")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(ItemEventContext context, AddNoteItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);

            profile.Pmc.Notes.Notes.Add(request.Note);

            return Task.CompletedTask;
        }
    }
}
