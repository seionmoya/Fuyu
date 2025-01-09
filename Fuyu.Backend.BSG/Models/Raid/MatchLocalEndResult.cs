using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Raid;

[DataContract]
public class MatchLocalEndResult
{
    [DataMember]
    public Profile profile { get; set; }

    [DataMember]
    public string result { get; set; }

    [DataMember]
    public string killerId { get; set; }

    [DataMember]
    public int? killerAid { get; set; }

    [DataMember]
    public string exitName { get; set; }

    [DataMember]
    public bool inSession { get; set; }

    [DataMember]
    public bool favorite { get; set; }

    [DataMember]
    public int playTime { get; set; }
}