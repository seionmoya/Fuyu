using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class EftStats
{
    [DataMember(EmitDefaultValue = false)]
    public Counter SessionCounters { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public Counter OverallCounters { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public float? SessionExperienceMult { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public float? ExperienceBonusMult { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public int? TotalSessionExperience { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public int? LastSessionDate { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public AggressorInfo Aggressor { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public PlacedQuestItem[] DroppedItems { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public FoundQuestItem[] FoundInRaidItems { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public VictimInfo[] Victims { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public MongoId[] CarriedQuestItems { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public DamageHistory DamageHistory { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public PlayerVisualRepresentation LastPlayerState { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public object DeathCause { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public long TotalInGameTime { get; set; }

    [DataMember]
    public ESurvivorClass SurvivorClass { get; set; }
}