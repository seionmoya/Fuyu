using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests
{
    [DataContract]
    public class MatchLocalStartRequest
    {
        [DataMember]
        public string location;

        [DataMember]
        public string timeVariant;

        [DataMember]
        public string mode;

        [DataMember]
        public string playerSide;
    }
}