using System.Runtime.Serialization;
using Fuyu.Backend.EFT.DTO.Accounts;

namespace Fuyu.Backend.EFT.DTO.Responses
{
    [DataContract]
    public class GameModeResponse
    {
        [DataMember(Name = "gameMode")]
        public ESessionMode? GameMode { get; set; }

        [DataMember(Name = "backendUrl")]
        public string BackendUrl { get; set; }
    }
}