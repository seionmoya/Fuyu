using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Raid
{
    [DataContract]
    public class MatchLocalEndResult
    {
        [DataMember]
        public Profile profile;

        [DataMember]
        public string result;

        [DataMember]
        public string killerId;

        [DataMember]
        public int? killerAid;

        [DataMember]
        public string exitName;

        [DataMember]
        public bool inSession;

        [DataMember]
        public bool favorite;

        [DataMember]
        public int playTime;
    }
}