using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Services;

namespace Fuyu.Backend.EFTMain.Controllers.ItemEvents;

public class RagFairAddOfferItemEventController : AbstractItemEventController<RagFairAddOfferItemEvent>
{
    private readonly RagfairService _ragfairService;
    private readonly EftOrm _eftOrm;
    private readonly ItemService _itemService;

    public RagFairAddOfferItemEventController() : base("RagFairAddOffer")
    {
        _ragfairService = RagfairService.Instance;
        _eftOrm = EftOrm.Instance;
        _itemService = ItemService.Instance;
    }

    public override Task RunAsync(ItemEventContext context, RagFairAddOfferItemEvent request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);
        var ragfairUser = new RagfairPlayerUser(profile.Pmc);
        var items = new List<ItemInstance>();

        foreach (var itemToSellId in request.Items)
        {
            var rootItem = profile.Pmc.Inventory.Items.Find(i => i.Id == itemToSellId);
            context.Response.ProfileChanges[profile.Pmc._id].Items.Delete.Add(rootItem);
            items.AddRange(_itemService.GetItemAndChildren(profile.Pmc.Inventory.Items, rootItem));
        }
        
        var offer = _ragfairService.CreateAndAddOffer(ragfairUser, items, request.SellAsPack, request.Requirements,
            TimeSpan.FromMinutes(30d), false);

        if (offer == null)
        {
            throw new Exception("Failed to create offer");
        }

        profile.Pmc.Inventory.Items.RemoveAll(items.Contains);

        return Task.CompletedTask;
    }
}