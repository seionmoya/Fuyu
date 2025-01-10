using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class Counter
{
    // TODO: proper type
    [DataMember]
    public List<KeyValuePair<List<string>, long>> Items { get; set; }
}