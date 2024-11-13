﻿using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemEvents;
using Fuyu.Backend.BSG.ItemEvents.Controllers;
using Fuyu.Backend.EFT.ItemEvents.Models;

namespace Fuyu.Backend.EFT.ItemEvents.Controllers
{
    public class EditNoteItemEventController : ItemEventController<EditNoteItemEvent>
    {
        public EditNoteItemEventController() : base("EditNote")
        {
        }

        public override Task RunAsync(ItemEventContext context, EditNoteItemEvent request)
        {
            var profile = EftOrm.GetActiveProfile(context.SessionId);
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
