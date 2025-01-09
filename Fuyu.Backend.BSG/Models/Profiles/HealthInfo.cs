using System;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Common;
using Fuyu.Backend.BSG.Models.Profiles.Health;

namespace Fuyu.Backend.BSG.Models.Profiles
{
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

        // SKIPPED: Immortal
        // Reason: only used on BSG's internal server
    }
}