using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using Fuyu.Backend.BSG.Models.Hideout;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class GetOtherProfileResponse
{
    // client expects string, eft backend sends int?
    [DataMember(Name = "aid")]
    public int AccountId { get; set; }

    [DataMember(Name = "info")]
    public PlayerInfo Info { get; set; }

    [DataMember(Name = "achievements")]
    public Dictionary<MongoId, int> Achievements { get; set; }

    [DataMember(Name = "customization")]
    public CustomizationInfo Customization { get; set; }

    [DataMember(Name = "favoriteItems")]
    public ItemInstance[] FavoriteItems { get; set; }

    [DataMember(Name = "equipment")]
    public ItemInfo Equipment { get; set; }

    [DataMember(Name = "pmcStats")]
    public SimpleStatsInfo PmcStats { get; set; }

    [DataMember(Name = "scavStats")]
    public SimpleStatsInfo ScavStats { get; set; }

    [DataMember(Name = "skills")]
    public SkillInfo Skills { get; set; }

    [DataMember(Name = "hideout")]
    public HideoutInfo Hideout { get; set; }

    [DataMember(Name = "hideoutAreaStashes", EmitDefaultValue = false)]
    public Dictionary<EAreaType, MongoId> HideoutAreaStashes { get; set; }

    [DataMember(Name = "customizationStash", EmitDefaultValue = false)]
    public MongoId CustomizationStash { get; set; }

    [DataMember(Name = "items", EmitDefaultValue = false)]
    public ItemInstance[] Items { get; set; }
}
