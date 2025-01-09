using System;
using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT;

public class TraderDatabase
{
    public static TraderDatabase Instance => instance.Value;
    private static readonly Lazy<TraderDatabase> instance = new(() => new TraderDatabase());

    private readonly ThreadDictionary<MongoId, TraderTemplate> _traders;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private TraderDatabase()
    {
        _traders = new ThreadDictionary<MongoId, TraderTemplate>();
    }

    public void Load()
    {
        var tradersJson = Resx.GetText("eft", "database.client.trading.api.traderSettings.json");
        var body = Json.Parse<ResponseBody<TraderTemplate[]>>(tradersJson);

        foreach (var traderTemplate in body.data)
        {
            _traders.Set(traderTemplate.Id, traderTemplate);
        }
    }

    public Dictionary<MongoId, TraderTemplate> GetTraderTemplates()
    {
        return _traders.ToDictionary();
    }
}