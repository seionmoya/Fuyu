using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class SpawnPointParam
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Vector3 Position { get; set; }

        [DataMember]
        public float Rotation { get; set; }

        [DataMember]
        public string[] Sides { get; set; }

        [DataMember]
        public string[] Categories { get; set; }

        [DataMember]
        public string Infiltration { get; set; }

        [DataMember]
        public float DelayToCanSpawnSec { get; set; }

        [DataMember]
        public ColliderParam ColliderParams { get; set; }

        [DataMember]
        public string BotZoneName { get; set; }

        [DataMember]
        public int CorePointId { get; set; }
    }
}