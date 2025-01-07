using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class GameProfileCreateResponse
    {
        [DataMember]
        public string uid { get; set; }
	}
}