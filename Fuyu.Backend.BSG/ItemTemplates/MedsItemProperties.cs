using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Collections;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class MedsItemProperties : ItemProperties
{
    [DataMember(Name = "medUseTime")]
    public float UseTime { get; set; }

    [DataMember(Name = "bodyPartTimeMults")]
    public KeyValuePair<EBodyPart, float>[] BodyPartTimeMults { get; set; }

    [DataMember(Name = "effects_health")]
    [UnionMappings(JTokenType.Object, JTokenType.Array)]
    public Union<Dictionary<string, HealthEffect>, object[]> HealthEffects { get; set; }

    [DataMember(Name = "effects_damage")]
    [UnionMappings(JTokenType.Object, JTokenType.Array)]
    public Union<Dictionary<string, DamageEffect>, object[]> DamageEffects { get; set; }

    [DataMember(Name = "StimulatorBuffs")]
    public string StimulatorBuffs { get; set; }

    [DataMember(Name = "MaxHpResource")]
    public int MaxHpResource { get; set; }

    [DataMember(Name = "hpResourceRate")]
    public float HpResourceRate { get; set; }
}

[DataContract]
public class HealthEffect
{
    [DataMember(Name = "delay")]
    public float Delay { get; set; }

    [DataMember(Name = "duration")]
    public float Duration { get; set; }
}

[DataContract]
public class DamageEffect
{
    [DataMember(Name = "delay")]
    public float Delay { get; set; }

    [DataMember(Name = "duration")]
    public float Duration { get; set; }

    [DataMember(Name = "fadeOut")]
    public float FadeOut { get; set; }

    [DataMember(Name = "cost")]
    public int Cost { get; set; }

    [DataMember(Name = "healthPenaltyMin")]
    public int HealthPenaltyMin { get; set; }

    [DataMember(Name = "healthPenaltyMax")]
    public int HealthPenaltyMax { get; set; }
}