using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class BossSpawn
    {
        [DataMember]
        public string BossName { get; set; }

        [DataMember]
        public int BossChance { get; set; }

        [DataMember]
        public string BossZone { get; set; }

        [DataMember]
        public bool BossPlayer { get; set; }

        [DataMember]
        public string BossDifficult { get; set; }

        [DataMember]
        public string BossEscortType { get; set; }

        [DataMember]
        public string BossEscortDifficult { get; set; }

        [DataMember]
        public string BossEscortAmount { get; set; }

        [DataMember]
        public int Time { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BossSupport[] Supports { get; set; }

        [DataMember]
        public bool RandomTimeSpawn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? ForceSpawn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? IgnoreMaxBots { get; set; }

        [DataMember]
        public string TriggerName { get; set; }

        [DataMember]
        public string TriggerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Delay { get; set; }

        // NOTE: server-side only
        [DataMember]
        public string[] SpawnMode { get; set; }
    }
}