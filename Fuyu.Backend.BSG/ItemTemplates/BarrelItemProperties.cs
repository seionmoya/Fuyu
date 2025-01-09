using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class BarrelItemProperties : MasterModItemProperties
{
    [DataMember(Name = "IsSilencer")]
    public bool IsSilencer;

    [DataMember(Name = "DurabilityBurnModificator")]
    public float DurabilityBurnModificator = 1f;

    [DataMember(Name = "HeatFactor")]
    public float HeatFactor = 1f;

    [DataMember(Name = "CoolFactor")]
    public float CoolFactor = 1f;

    [DataMember(Name = "CenterOfImpact")]
    public float CenterOfImpact;

    [DataMember(Name = "ShotgunDispersion")]
    public float ShotgunDispersion;

    [DataMember(Name = "DeviationMax")]
    public float DeviationMax;

    [DataMember(Name = "DeviationCurve")]
    public float DeviationCurve;
}