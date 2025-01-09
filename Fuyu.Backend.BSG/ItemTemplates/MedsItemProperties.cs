using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Collections;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class MedsItemProperties : ItemProperties
{
    [DataMember(Name = "medUseTime")]
    public float UseTime;

    [DataMember(Name = "bodyPartTimeMults")]
    public KeyValuePair<EBodyPart, float>[] BodyPartTimeMults;

    [DataMember(Name = "effects_health")]
    public Union<Dictionary<string, HealthEffect>, object[]> HealthEffects;

    [DataMember(Name = "effects_damage")]
    public Union<Dictionary<string, DamageEffect>, object[]> DamageEffects;

    [DataMember(Name = "StimulatorBuffs")]
    public string StimulatorBuffs;

    [DataMember(Name = "MaxHpResource")]
    public int MaxHpResource;

    [DataMember(Name = "hpResourceRate")]
    public float HpResourceRate;
}

public class HealthEffect
{
}

public class DamageEffect
{
}