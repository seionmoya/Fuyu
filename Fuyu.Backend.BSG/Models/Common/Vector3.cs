using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common
{
    [DataContract]
    public class Vector3
    {
        [DataMember]
        public float x;

        [DataMember]
        public float y;

        [DataMember]
        public float z;
    }
}