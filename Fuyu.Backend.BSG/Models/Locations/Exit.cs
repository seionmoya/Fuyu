using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class Exit
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string EntryPoints { get; set; }

    [DataMember]
    public int Chance { get; set; }

    [DataMember]
    public int MinTime { get; set; }

    [DataMember]
    public int MaxTime { get; set; }

    [DataMember]
    public int PlayersCount { get; set; }

    [DataMember]
    public int ExfiltrationTime { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public string PassageRequirement { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public string ExfiltrationType { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public string RequiredSlot { get; set; }

    [DataMember]
    public string Id { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public string RequirementTip { get; set; }

    [DataMember]
    public int Count { get; set; }

    [DataMember]
    public bool EventAvailable { get; set; }

    [DataMember]
    public int MinTimePVE { get; set; }

    [DataMember]
    public int MaxTimePVE { get; set; }

    [DataMember]
    public int ChancePVE { get; set; }

    [DataMember]
    public int CountPVE { get; set; }

    [DataMember]
    public int ExfiltrationTimePVE { get; set; }

    [DataMember]
    public int PlayersCountPVE { get; set; }
}