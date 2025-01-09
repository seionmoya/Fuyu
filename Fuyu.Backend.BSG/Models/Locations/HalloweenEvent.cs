using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class HalloweenEvent
    {
        [DataMember]
        public int InfectionPercentage { get; set; }

        [DataMember]
        public int MinInfectionPercentage { get; set; }

        [DataMember]
        public List<CrowdAttackSpawnParameters> CrowdAttackSpawnParams { get; set; }

        [DataMember]
        public float ZombieMultiplier { get; set; }

        [DataMember]
        public int MaxCrowdAttackSpawnLimit { get; set; }

        [DataMember]
        public int CrowdsLimit { get; set; }

        [DataMember]
        public float InfectedLookCoeff { get; set; }

        [DataMember]
        public float CrowdCooldownPerPlayerSec { get; set; }

        [DataMember]
        public float ZombieCallPeriodSec { get; set; }

        [DataMember]
        public float ZombieCallDeltaRadius { get; set; }

        [DataMember]
        public float ZombieCallRadiusLimit { get; set; }

        [DataMember]
        public float CrowdAttackBlockRadius { get; set; }

        [DataMember]
        public float MinSpawnDistToPlayer { get; set; }

        [DataMember]
        public float TargetPointSearchRadiusLimit { get; set; }
    }
}