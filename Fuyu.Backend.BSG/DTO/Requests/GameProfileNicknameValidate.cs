using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Requests
{
    [DataContract]
    public class GameProfileNicknameValidateRequest
    {
        [DataMember]
        public string nickname;
    }
}