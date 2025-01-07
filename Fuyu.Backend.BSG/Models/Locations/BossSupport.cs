using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class BossSupport
    {
        [DataMember]
        public string BossEscortType { get; set; }

        [DataMember]
        public string[] BossEscortDifficult { get; set; }

        [DataMember]
        public string BossEscortAmount { get; set; }
    }
}