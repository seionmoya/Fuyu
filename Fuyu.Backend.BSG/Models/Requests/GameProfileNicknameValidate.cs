using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class GameProfileNicknameValidateRequest
{
    [DataMember]
    public string nickname { get; set; }
}