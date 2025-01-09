using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class MatchLocalStartRequest
{
    [DataMember]
    public string location { get; set; }

    [DataMember]
    public string timeVariant { get; set; }

    [DataMember]
    public string mode { get; set; }

    [DataMember]
    public string playerSide { get; set; }
}