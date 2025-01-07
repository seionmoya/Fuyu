using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Hideout;

namespace Fuyu.Backend.BSG.Models.Profiles.Hideout
{
    [DataContract]
    public class HideoutAreaInfo
    {
        [DataMember]
        public EAreaType type { get; set; }

        [DataMember]
        public int level { get; set; }

        [DataMember]
        public bool active { get; set; }

        // TODO: proper type
        [DataMember]
        public bool passiveBonusesEnabled { get; set; }

        [DataMember]
        public long completeTime { get; set; }

        [DataMember]
        public bool conclassing { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] slots { get; set; }

        [DataMember]
        public string lastRecipe { get; set; }
    }
}