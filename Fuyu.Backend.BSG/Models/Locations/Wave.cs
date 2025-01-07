using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class Wave
    {
        [DataMember]
        public int number { get; set; }

        [DataMember]
        public int time_min { get; set; }

        [DataMember]
        public int time_max { get; set; }

        [DataMember]
        public int slots_min { get; set; }

        [DataMember]
        public int slots_max { get; set; }

        [DataMember]
        public string SpawnPoints { get; set; }

        [DataMember]
        public string BotSide { get; set; }

        [DataMember]
        public string BotPreset { get; set; }

        [DataMember]
        public bool isPlayers { get; set; }

        [DataMember]
        public string WildSpawnType { get; set; }

        // Tracks which waves to send for which game mode
        // NOTE: server-side only
        [DataMember]
        public string[] SpawnMode { get; set; }
    }
}