using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class GameProfileNicknameChangeResponse
{
    [DataMember(Name = "status")]
    public string Status { get; set; }
}