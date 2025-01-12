using System;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Bots;
using Fuyu.Backend.BSG.Models.Profiles.Info;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class VictimInfo
{
    [DataMember(Name = "AccountId")]
    public string AccountId { get; set; }

    [DataMember(Name = "ProfileId")]
    public string ProfileId { get; set; }

    [DataMember(Name = "Name")]
    public string Name { get; set; }

    [DataMember(Name = "Side")]
    public EPlayerSide Side { get; set; }

    [DataMember(Name = "Time")]
    public TimeSpan Time { get; set; }

    [DataMember(Name = "Level")]
    public int Level { get; set; }

    [DataMember(Name = "PrestigeLevel")]
    public int PrestigeLevel { get; set; }

    [DataMember(Name = "BodyPart")]
    public EBodyPart BodyPart { get; set; }

    [DataMember(Name = "Weapon")]
    public string Weapon { get; set; }

    [DataMember(Name = "Distance")]
    public float Distance { get; set; }

    [DataMember(Name = "Role")]
    public EWildSpawnType Role { get; set; }

    [DataMember(Name = "Location")]
    public string Location { get; set; }
}