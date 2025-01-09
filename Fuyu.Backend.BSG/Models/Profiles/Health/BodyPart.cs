using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Profiles.Health;

[DataContract]
public class BodyPart
{
    [DataMember]
    public ClampedValue<float> Health { get; set; }

    [DataMember]
    public Dictionary<string, BodyPartEffect> Effects { get; set; }
}