using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class CheckVersionResponse
    {
        [DataMember]
        public bool isvalid;

        [DataMember]
        public string latestVersion;
    }
}