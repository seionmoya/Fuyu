using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

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