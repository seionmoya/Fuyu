using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Multiplayer;

[DataContract]
public class ProfileStatusInfo
{
    [DataMember]
    public string profileid { get; set; }

    [DataMember]
    public string profileToken { get; set; }

    [DataMember]
    public string status { get; set; }

    [DataMember]
    public string sid { get; set; }

    [DataMember]
    public string ip { get; set; }

    [DataMember]
    public int port { get; set; }
}