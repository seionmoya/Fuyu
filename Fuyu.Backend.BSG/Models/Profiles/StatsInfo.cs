using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Stats;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class StatsInfo
{
    [DataMember(Name = "eft", EmitDefaultValue = false)]
    public EftStats Eft { get; set; }

    [DataMember(Name = "arena", EmitDefaultValue = false)]
    public EftStats Arena { get; set; }

    // TODO: Other implementation + Arena info
    public SimpleStatsInfo ToSimpleStatsInfo()
    {
        return new SimpleStatsInfo
        {
            Eft = new SimpleEftStats()
            {
                OverallCounters = Eft.OverallCounters,
                TotalInGameTime = Eft.TotalInGameTime
            }
        };
    }
}