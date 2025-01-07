using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Hideout;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class HideoutInfo
    {
        // TODO: proper type
        [DataMember]
        public object Production { get; set; }

        [DataMember]
        public HideoutAreaInfo[] Areas { get; set; }

        // TODO: proper type
        [DataMember]
        public object Improvements { get; set; }

        [DataMember]
        public long Seed { get; set; }
    }
}