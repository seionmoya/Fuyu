using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class GameProfileNicknameChangeResponse
{
    [DataMember(Name = "status")]
    public ENicknameChangeResult Status { get; set; }
}