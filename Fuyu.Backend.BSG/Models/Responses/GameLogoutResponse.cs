using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class GameLogoutResponse
    {
        [DataMember]
        public string status { get; set; }
	}
}