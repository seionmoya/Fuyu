using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class GameProfileNicknameValidateRequest
{
    [DataMember(Name = "nickname")]
    public string Nickname { get; set; }
}