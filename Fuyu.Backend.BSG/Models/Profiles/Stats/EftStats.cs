using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats
{
    [DataContract]
    public class EftStats
    {
        [DataMember]
        public Counter SessionCounters { get; set; }

        [DataMember]
        public Counter OverallCounters { get; set; }

        [DataMember]
        public int SessionExperienceMult { get; set; }

        [DataMember]
        public int ExperienceBonusMult { get; set; }

        [DataMember]
        public int TotalSessionExperience { get; set; }

        [DataMember]
        public long LastSessionDate { get; set; }

        // TODO: proper type
        [DataMember]
        public object Aggressor { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] DroppedItems { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] FoundInRaidItems { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] Victims { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] CarriedQuestItems { get; set; }

        [DataMember]
        public DamageHistory DamageHistory { get; set; }

        // TODO: proper type
        [DataMember]
        public object LastPlayerState { get; set; }

        [DataMember]
        public int TotalInGameTime { get; set; }

        [DataMember]
        public string SurvivorClass { get; set; }
    }
}