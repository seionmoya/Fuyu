using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class GasblockItemProperties : FunctionalModItemProperties
{
    [DataMember(Name = "DurabilityBurnModificator")]
    public float DurabilityBurnModificator { get; set; } = 1f;

    [DataMember(Name = "HeatFactor")]
    public float HeatFactor { get; set; } = 1f;

    [DataMember(Name = "CoolFactor")]
    public float CoolFactor { get; set; } = 1f;
}