using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common
{
    [DataContract]
    public class BundleAddress
    {
        [DataMember]
        public string path;

        [DataMember]
        public string rcid;
    }
}