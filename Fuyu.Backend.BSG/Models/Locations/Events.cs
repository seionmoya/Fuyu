using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class Events
{
    [DataMember]
    public HalloweenEvent Halloween2024 { get; set; }

    [DataMember]
    public KhorovodEvent Khorovod { get; set; }
}