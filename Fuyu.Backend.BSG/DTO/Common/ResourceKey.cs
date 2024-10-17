using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Common
{
    [DataContract]
    public class ResourceKey
    {
        [DataMember]
        public string path;

        [DataMember]
        public string rcid;
    }
}