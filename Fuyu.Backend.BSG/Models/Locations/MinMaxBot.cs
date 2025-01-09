using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class MinMaxBot
{
    [DataMember]
    public int min { get; set; }

    [DataMember]
    public int max { get; set; }

    [DataMember]
    public string WildSpawnType { get; set; }
}