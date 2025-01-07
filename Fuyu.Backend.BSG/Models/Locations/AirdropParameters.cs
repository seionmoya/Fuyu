using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class AirdropParameters
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public int PlaneAirdropStartMin { get; set; }

        [DataMember]
        public int PlaneAirdropStartMax { get; set; }

        [DataMember]
        public int PlaneAirdropEnd { get; set; }

        [DataMember]
        public float PlaneAirdropChance { get; set; }

        [DataMember]
        public int PlaneAirdropMax { get; set; }

        [DataMember]
        public int PlaneAirdropCooldownMin { get; set; }

        [DataMember]
        public int PlaneAirdropCooldownMax { get; set; }

        [DataMember]
        public int AirdropPointDeactivateDistance { get; set; }

        [DataMember]
        public int MinPlayersCountToSpawnAirdrop { get; set; }

        [DataMember]
        public int UnsuccessfulTryPenalty { get; set; }
    }
}