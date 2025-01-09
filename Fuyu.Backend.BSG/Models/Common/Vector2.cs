using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Common;

[DataContract]
public class Vector2
{
    [DataMember(Name = "x")]
    public float X { get; set; }

    [DataMember(Name = "y")]
    public float Y { get; set; }
}