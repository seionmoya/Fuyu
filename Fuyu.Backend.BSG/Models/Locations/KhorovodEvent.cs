using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class KhorovodEvent
{
    [DataMember]
    public int Chance { get; set; }
}