using System;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Common;
using Fuyu.Backend.BSG.Models.Profiles.Health;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class HealthInfo
{
    [DataMember]
    public ClampedValue<float> Hydration { get; set; }

    [DataMember]
    public ClampedValue<float> Energy { get; set; }

    [DataMember]
    public ClampedValue<float> Temperature { get; set; }

    [DataMember]
    public ClampedValue<float> Poison { get; set; }

    [DataMember]
    public BodyPartInfo BodyParts { get; set; }

    [DataMember]
    public int? UpdateTime { get; set; }

    public BodyPart GetBodyPart(EBodyPart bodyPart)
    {
        return bodyPart switch
        {
            EBodyPart.Head => BodyParts.Head,
            EBodyPart.Chest => BodyParts.Chest,
            EBodyPart.Stomach => BodyParts.Stomach,
            EBodyPart.LeftArm => BodyParts.LeftArm,
            EBodyPart.RightArm => BodyParts.RightArm,
            EBodyPart.LeftLeg => BodyParts.LeftLeg,
            EBodyPart.RightLeg => BodyParts.RightLeg,
            EBodyPart.Common => throw new NotImplementedException("Cannot use Common?"),
            _ => null,
        };
    }

    /// <summary>
    /// Returns whether any <see cref="BodyPart"/> contain a <see cref="BodyPartEffect"/>
    /// </summary>
    [IgnoreDataMember]
    public bool HasEffects
    {
        get
        {
            return BodyParts.Any(x => x.Effects.Count > 0);
        }
    }

    // SKIPPED: Immortal
    // Reason: only used on BSG's internal server
}