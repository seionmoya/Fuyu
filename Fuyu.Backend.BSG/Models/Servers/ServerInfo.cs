using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Servers
{
    [DataContract]
    public class ServerInfo
    {
        [DataMember]
        public string ip { get; set; }

        [DataMember]
        public int port { get; set; }
    }
}