using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class EftStats
{
    [DataMember]
    public Counter SessionCounters { get; set; }

    [DataMember]
    public Counter OverallCounters { get; set; }

    [DataMember]
    public float SessionExperienceMult { get; set; }

    [DataMember]
    public float ExperienceBonusMult { get; set; }

    [DataMember]
    public int TotalSessionExperience { get; set; }

    [DataMember]
    public int LastSessionDate { get; set; }

    // TODO: proper type
    [DataMember]
    public object Aggressor { get; set; }

    // TODO: proper type
    [DataMember]
    public object[] DroppedItems { get; set; }

    // TODO: proper type
    [DataMember]
    public object[] FoundInRaidItems { get; set; }

    // TODO: proper type
    [DataMember]
    public object[] Victims { get; set; }

    [DataMember]
    public MongoId[] CarriedQuestItems { get; set; }

    [DataMember]
    public DamageHistory DamageHistory { get; set; }

    // TODO: proper type
    [DataMember]
    public object LastPlayerState { get; set; }

    [DataMember]
    public long TotalInGameTime { get; set; }

    [DataMember]
    public string SurvivorClass { get; set; }
}