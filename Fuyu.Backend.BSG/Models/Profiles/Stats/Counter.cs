using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class Counter
{
    // TODO: proper type
    [DataMember]
    public object[] Items { get; set; }
}