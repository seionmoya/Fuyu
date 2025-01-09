using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Hideout;

[DataContract]
public class HideoutAreaSlot
{
    // TODO: proper type
    [DataMember]
    public object item { get; set; }
}