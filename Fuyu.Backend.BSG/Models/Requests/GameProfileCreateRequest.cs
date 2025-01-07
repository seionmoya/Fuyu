using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests
{
    [DataContract]
    public class GameProfileCreateRequest
    {
        [DataMember]
        public string side { get; set; }

        [DataMember]
        public string nickname { get; set; }

        [DataMember]
        public string headId { get; set; }

        [DataMember]
        public string voiceId { get; set; }
    }
}