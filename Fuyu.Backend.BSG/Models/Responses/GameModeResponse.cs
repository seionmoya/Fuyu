using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Accounts;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class GameModeResponse
{
    [DataMember(Name = "gameMode")]
    public ESessionMode? GameMode { get; set; }

    [DataMember(Name = "backendUrl")]
    public string BackendUrl { get; set; }
}