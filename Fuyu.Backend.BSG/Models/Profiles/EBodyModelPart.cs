using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

public enum EBodyModelPart
{
    [DataMember(Name = "body")]
    Body,
    [DataMember(Name = "feet")]
    Feet,
    [DataMember(Name = "head")]
    Head,
    [DataMember(Name = "hands")]
    Hands,
    [DataMember(Name = "dogtag")]
    DogTag
}