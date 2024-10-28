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
	public class RemoveFromWishListItemEventController : ItemEventController<RemoveFromWishListItemEvent>
	{
		public RemoveFromWishListItemEventController() : base("RemoveFromWishList")
		{
		}

		public override Task RunAsync(ItemEventContext context, RemoveFromWishListItemEvent request)
		{
			return Task.CompletedTask;
		}
	}
}
