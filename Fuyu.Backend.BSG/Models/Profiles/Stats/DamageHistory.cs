using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Profiles.Health;
using Fuyu.Common.Collections;

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
    public Union<Dictionary<string, List<BodyPartDamage>>, List<BodyPartDamage>> BodyParts { get; set; }
}

public class BodyPartDamage
{
    [DataMember(Name = "Amount")]
    public float Amount;

    [DataMember(Name = "Type")]
    public EDamageType Type;

    [DataMember(Name = "SourceId")]
    public string SourceId;

    [DataMember(Name = "OverDamageFrom")]
    public EBodyPart? OverDamageFrom;

    [DataMember(Name = "Blunt")]
    public bool Blunt;

    [DataMember(Name = "ImpactsCount")]
    public float ImpactsCount;
}

[Flags]
public enum EDamageType
{
    Undefined = 1,
    Fall = 2,
    Explosion = 4,
    Barbed = 8,
    Flame = 16,
    GrenadeFragment = 32,
    Impact = 64,
    Existence = 128,
    Medicine = 256,
    Bullet = 512,
    Melee = 1024,
    Landmine = 2048,
    Sniper = 4096,
    Blunt = 8192,
    LightBleeding = 16384,
    HeavyBleeding = 32768,
    Dehydration = 65536,
    Exhaustion = 131072,
    RadExposure = 262144,
    Stimulator = 524288,
    Poison = 1048576,
    LethalToxin = 2097152,
    Btr = 4194304,
    Artillery = 8388608,
    Environment = 16777216
}