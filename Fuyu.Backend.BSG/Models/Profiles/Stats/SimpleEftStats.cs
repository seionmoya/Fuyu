using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class SimpleEftStats
{
    [DataMember(Name = "overAllCounters", EmitDefaultValue = false)]
    public Counter OverallCounters { get; set; }

    [DataMember(Name = "totalInGameTime", EmitDefaultValue = false)]
    public long TotalInGameTime { get; set; }
}