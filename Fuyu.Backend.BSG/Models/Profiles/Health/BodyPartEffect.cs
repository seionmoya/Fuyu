using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Health;

[DataContract]
public class BodyPartEffect
{
    [DataMember]
    public float Time { get; set; } = -1f;
}