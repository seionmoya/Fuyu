using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Stats;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class SimpleStatsInfo
{
    [DataMember(Name = "eft", EmitDefaultValue = false)]
    public SimpleEftStats Eft { get; set; }

    [DataMember(Name = "arena", EmitDefaultValue = false)]
    public SimpleEftStats Arena { get; set; }
}