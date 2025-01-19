using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Health;
using Fuyu.Common.Collections;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class DamageHistory
{
    [DataMember]
    public string LethalDamagePart { get; set; }

    // TODO: proper type
    [DataMember]
    public object LethalDamage { get; set; }

    [DataMember]
    [UnionMappings(JTokenType.Object, JTokenType.Array)]
    public Union<Dictionary<string, List<BodyPartDamage>>, List<BodyPartDamage>> BodyParts { get; set; }
}