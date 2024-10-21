using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Common
{
    [DataContract]
    public struct Vector3
    {
        [DataMember(Name = "x")]
        public float X { get; set; }

        [DataMember(Name = "y")]
        public float Y { get; set; }

        [DataMember(Name = "z")]
        public float Z { get; set; }
    }
}