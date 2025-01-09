using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class GameProfileVoiceChangeRequest
{
    [DataMember(Name = "voice")]
    public string Voice { get; set; }
}
