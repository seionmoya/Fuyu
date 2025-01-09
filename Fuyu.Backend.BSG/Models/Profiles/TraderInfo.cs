using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class TraderInfo
{
    [DataMember]
    public bool unlocked { get; set; }

    [DataMember]
    public bool disabled { get; set; }

    [DataMember]
    public int salesSum { get; set; }

    [DataMember]
    public float standing { get; set; }
}