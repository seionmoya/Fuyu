using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class GroupScenario
{
    [DataMember]
    public int MinToBeGroup { get; set; }

    [DataMember]
    public int MaxToBeGroup { get; set; }

    [DataMember]
    public int Chance { get; set; }

    [DataMember]
    public bool Enabled { get; set; }
}