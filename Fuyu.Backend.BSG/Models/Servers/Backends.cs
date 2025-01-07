using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Servers
{
    [DataContract]
    public class Backends
    {
        [DataMember]
        public string Lobby { get; set; }

        [DataMember]
        public string Trading { get; set; }

        [DataMember]
        public string Messaging { get; set; }

        [DataMember]
        public string Main { get; set; }

        [DataMember]
        public string RagFair { get; set; }
    }
}