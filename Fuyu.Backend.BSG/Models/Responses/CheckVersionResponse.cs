using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class CheckVersionResponse
    {
        [DataMember]
        public bool isvalid { get; set; }

        [DataMember]
        public string latestVersion { get; set; }
    }
}