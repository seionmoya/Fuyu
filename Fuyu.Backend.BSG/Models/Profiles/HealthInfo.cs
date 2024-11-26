using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;
using Fuyu.Backend.BSG.Models.Profiles.Health;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class HealthInfo
    {
        [DataMember]
        public CurrentMaximum<float> Hydration;

        [DataMember]
        public CurrentMaximum<float> Energy;

        [DataMember]
        public CurrentMaximum<float> Temperature;

        [DataMember]
        public BodyPartInfo BodyParts;

        [DataMember]
        public int? UpdateTime;

        // SKIPPED: Immortal
        // Reason: only used on BSG's internal server
    }
}