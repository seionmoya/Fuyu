using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Bots;
using Fuyu.Backend.BSG.Models.Profiles.Info;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class AggressorInfo
{
    [DataMember(Name = "AccountId")]
    public string AccountId { get; set; }

    [DataMember(Name = "ProfileId")]
    public string ProfileId { get; set; }

    [DataMember(Name = "MainProfileNickname")]
    public string MainProfileNickname { get; set; }

    [DataMember(Name = "Name")]
    public string Name { get; set; }

    [DataMember(Name = "Side")]
    public EPlayerSide Side { get; set; }

    [DataMember(Name = "PrestigeLevel")]
    public int PrestigeLevel { get; set; }

    [DataMember(Name = "ColliderType")]
    public EBodyPartColliderType ColliderType { get; set; }

    [DataMember(Name = "WeaponName")]
    public string WeaponName { get; set; }

    [DataMember(Name = "Category")]
    public EMemberCategory Category { get; set; }

    [DataMember(Name = "Role")]
    public EWildSpawnType Role { get; set; }
}