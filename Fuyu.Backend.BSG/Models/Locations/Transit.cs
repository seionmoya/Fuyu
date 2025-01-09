using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class Transit
{
    [DataMember]
    public int id { get; set; }

    [DataMember]
    public bool active { get; set; }

    [DataMember]
    public string name { get; set; }

    [DataMember]
    public string location { get; set; }

    [DataMember]
    public string description { get; set; }

    [DataMember]
    public int activateAfterSec { get; set; }

    [DataMember]
    public string target { get; set; }

    [DataMember]
    public int time { get; set; }

    [DataMember]
    public string conditions { get; set; }
}