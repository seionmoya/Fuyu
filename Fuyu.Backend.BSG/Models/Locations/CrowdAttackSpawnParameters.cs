using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class CrowdAttackSpawnParameters
    {
        [DataMember]
        public EBotDifficulty Difficulty { get; set; }

        [DataMember]
        public EBotRole Role { get; set; }

    }
}