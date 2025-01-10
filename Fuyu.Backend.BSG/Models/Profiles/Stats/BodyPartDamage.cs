using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

public class BodyPartDamage
{
    [DataMember(Name = "Amount")]
    public float Amount;

    [DataMember(Name = "Type")]
    public EDamageType Type;

    [DataMember(Name = "SourceId")]
    public string SourceId;

    [DataMember(Name = "OverDamageFrom")]
    public EBodyPart? OverDamageFrom;

    [DataMember(Name = "Blunt")]
    public bool Blunt;

    [DataMember(Name = "ImpactsCount")]
    public float ImpactsCount;
}