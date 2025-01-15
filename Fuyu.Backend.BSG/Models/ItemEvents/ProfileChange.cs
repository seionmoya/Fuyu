using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class ProfileChange
{
    public ProfileChange()
    {
        UnlockedRecipes = [];
        Items = new ItemChanges();
        TradersData = [];
    }

    [DataMember(Name = "experience")]
    public int Experience { get; set; }

    [DataMember(Name = "recipeUnlocked")]
    public Dictionary<MongoId, bool> UnlockedRecipes { get; set; }

    [DataMember(Name = "items")]
    public ItemChanges Items { get; set; }

    [DataMember(Name = "traderRelations")]
    public Dictionary<MongoId, TraderData> TradersData { get; set; }
}

[DataContract]
public class TraderData
{
    [DataMember(Name = "salesSum")]
    public long? _salesSum { get; set; }

    [DataMember(Name = "standing")]
    public double? _standing { get; set; }

    [DataMember(Name = "loyalty")]
    public float? _loyaltyLevel { get; set; }

    [DataMember(Name = "unlocked")]
    public bool? _unlocked { get; set; }

    [DataMember(Name = "disabled")]
    public bool? _disabled { get; set; }
}