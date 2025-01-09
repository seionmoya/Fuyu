using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class BackpackItemProperties : SearchableItemItemProperties
{
    [DataMember(Name = "LeanWeaponAgainstBody")]
    public bool LeanWeaponAgainstBody;

    [DataMember(Name = "GridLayoutName")]
    public string GridLayoutName;

    [DataMember(Name = "speedPenaltyPercent")]
    public float speedPenaltyPercent;

    [DataMember(Name = "mousePenalty")]
    public float mousePenalty;

    [DataMember(Name = "weaponErgonomicPenalty")]
    public float weaponErgonomicPenalty;
}