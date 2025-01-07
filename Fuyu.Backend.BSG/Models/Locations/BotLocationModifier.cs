using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class BotLocationModifier
    {
        [DataMember]
        public float AccuracySpeed { get; set; }

        [DataMember]
        public float Scattering { get; set; }

        [DataMember]
        public float GainSight { get; set; }

        [DataMember]
        public float MarksmanAccuratyCoef { get; set; }

        [DataMember]
        public float VisibleDistance { get; set; }

        [DataMember]
        public float DistToPersueAxemanCoef { get; set; }

        [DataMember]
        public float DistToSleep { get; set; }

        [DataMember]
        public float DistToActivate { get; set; }

        [DataMember]
        public float MagnetPower { get; set; }

        [DataMember]
        public float KhorovodChance { get; set; }
    }
}