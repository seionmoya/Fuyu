using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int InfectionPercentage;

        [DataMember]
        public int MinInfectionPercentage;

        [DataMember]
        public List<CrowdAttackSpawnParameters> CrowdAttackSpawnParams;

        [DataMember]
        public float ZombieMultiplier;

        [DataMember]
        public int MaxCrowdAttackSpawnLimit;

        [DataMember]
        public int CrowdsLimit;

        [DataMember]
        public float InfectedLookCoeff;

        [DataMember]
        public float CrowdCooldownPerPlayerSec;

        [DataMember]
        public float ZombieCallPeriodSec;

        [DataMember]
        public float ZombieCallDeltaRadius;

        [DataMember]
        public float ZombieCallRadiusLimit;

        [DataMember]
        public float CrowdAttackBlockRadius;

        [DataMember]
        public float MinSpawnDistToPlayer;

        [DataMember]
        public float TargetPointSearchRadiusLimit;
    }
}