using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;
using Fuyu.Backend.BSG.Models.Profiles.Health;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class HealthInfo
{
    [DataMember]
    public CurrentMaximum<float> Hydration { get; set; }

    [DataMember]
    public CurrentMaximum<float> Energy { get; set; }

    [DataMember]
    public CurrentMaximum<float> Temperature { get; set; }

    [DataMember]
    public BodyPartInfo BodyParts { get; set; }

    [DataMember]
    public int? UpdateTime { get; set; }

    // SKIPPED: Immortal
    // Reason: only used on BSG's internal server
}