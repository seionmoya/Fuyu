using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class CrowdAttackSpawnParameters
    {
        [DataMember]
        public EBotDifficulty Difficulty;

        [DataMember]
        public EBotRole Role;

        [DataMember]
        public int Weight;
    }
}