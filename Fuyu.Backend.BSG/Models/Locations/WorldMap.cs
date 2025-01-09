using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class WorldMap
{
    [DataMember]
    public Dictionary<string, Location> locations { get; set; }

    [DataMember]
    public Path[] paths { get; set; }
}