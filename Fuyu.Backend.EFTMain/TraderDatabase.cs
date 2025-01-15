using System;
using System.Collections.Generic;
using System.IO;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Services;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFTMain;

public class TraderDatabase
{
    public static TraderDatabase Instance => instance.Value;
    private static readonly Lazy<TraderDatabase> instance = new(() => new TraderDatabase());

    private readonly ThreadDictionary<MongoId, TraderTemplate> _traders;
    private readonly ThreadDictionary<MongoId, TraderAssort> _traderAssort;

    private readonly RagfairService _ragfairService;
    private readonly ItemService _itemService;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private TraderDatabase()
    {
        _traders = new ThreadDictionary<MongoId, TraderTemplate>();
        _traderAssort = new ThreadDictionary<MongoId, TraderAssort>();
        _ragfairService = RagfairService.Instance;
        _itemService = ItemService.Instance;
    }

    public void Load()
    {
        var tradersJson = Resx.GetText("eft", "database.client.trading.api.traderSettings.json");
        var body = Json.Parse<ResponseBody<TraderTemplate[]>>(tradersJson);

        foreach (var traderTemplate in body.data)
        {
            _traders.Set(traderTemplate.Id, traderTemplate);
            try
            {
                var assortJson = Resx.GetText("eft",
                    $"database.client.trading.api.getTraderAssort.{traderTemplate.Id}.json");
                var traderAssort = Json.Parse<TraderAssort>(assortJson);
                _traderAssort.Set(traderTemplate.Id, traderAssort);

                var traderRagfairUser = new RagfairTraderUser(traderTemplate.Id);

                foreach (var (itemId, scheme) in traderAssort.BarterScheme)
                {
                    var items = _itemService.GetItemAndChildren(traderAssort.Items, itemId);
                    var handOverRequirements = new List<HandoverRequirement>();
                    var loyaltyLevel = traderAssort.LoyaltyLevelItems[itemId];

                    foreach (var requirement in scheme)
                    {
                        foreach (var requirement2 in requirement)
                        {
                            handOverRequirements.Add(new HandoverRequirement
                            {
                                Count = (int)requirement2.Count,
                                TemplateId = requirement2.Template
                            });
                        }
                    }

                    _ragfairService.CreateAndAddOffer(traderRagfairUser, items, false, handOverRequirements, TimeSpan.FromHours(1d), false, loyaltyLevel);
                }

                Terminal.WriteLine($"Got assort for {traderTemplate.Id}");
            }
            catch (FileNotFoundException)
            {
                Terminal.WriteLine($"Failed to get assort for {traderTemplate.Id}");
            }
        }
    }

    public Dictionary<MongoId, TraderTemplate> GetTraderTemplates()
    {
        return _traders.ToDictionary();
    }

    public TraderAssort GetTraderAssort(MongoId traderId)
    {
        if (!_traderAssort.TryGet(traderId, out TraderAssort result))
        {
            result = null;
        }

        return result;
    }
}