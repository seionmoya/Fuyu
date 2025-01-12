using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class Counter
{
    // NOTE: KeyValuePair could be wrong, I did it to avoid having a custom type
    [DataMember(EmitDefaultValue = false)]
    public List<KeyValuePair<List<string>, long>> Items { get; set; }
}