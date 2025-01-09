using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class BaseItemEvent
{
    [DataMember(Name = "Action")]
    public string Action { get; }
}