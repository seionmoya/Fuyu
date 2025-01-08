﻿using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Networking;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class RemoveFromWishListItemEventController : AbstractItemEventController<RemoveFromWishListItemEvent>
    {
        public RemoveFromWishListItemEventController() : base("RemoveFromWishList")
        {
        }

        public override Task RunAsync(ItemEventContext context, RemoveFromWishListItemEvent request)
        {
            var profile = EftOrm.Instance.GetActiveProfile(context.SessionId);
            var wishList = profile.Pmc.GetWishList();

            foreach (var itemToRemove in request.Items)
            {
                wishList.Remove(itemToRemove);
            }

            return Task.CompletedTask;
        }
    }
}
