using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class GameProfileNicknameValidateResponse
    {
        [DataMember]
        public string status { get; set; }
	}
}