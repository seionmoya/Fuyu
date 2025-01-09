using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Health;

[DataContract]
public class BodyPart
{
    [DataMember]
    public ClampedHealthStat<float> Health { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public Dictionary<string, BodyPartEffect> Effects { get; set; }
}