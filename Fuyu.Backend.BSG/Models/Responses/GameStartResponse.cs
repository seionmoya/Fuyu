using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class GameStartResponse
    {
        [DataMember]
        public double utc_time { get; set; }
	}
}