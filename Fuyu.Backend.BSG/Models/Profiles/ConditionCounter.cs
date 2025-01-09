using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class ConditionCounter
{
    [DataMember]
    public string id { get; set; }

    [DataMember]
    public string sourceId { get; set; }

    [DataMember]
    public string type { get; set; }

    [DataMember]
    public int value { get; set; }
}