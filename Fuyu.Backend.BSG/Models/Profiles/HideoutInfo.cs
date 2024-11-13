using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Hideout;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class HideoutInfo
    {
        // TODO: proper type
        [DataMember]
        public object Production;

        [DataMember]
        public HideoutAreaInfo[] Areas;

        // TODO: proper type
        [DataMember]
        public object Improvements;

        [DataMember]
        public long Seed;
    }
}