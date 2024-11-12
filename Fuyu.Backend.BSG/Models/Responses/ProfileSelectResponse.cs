using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class ProfileSelectResponse
    {
        [DataMember]
        public string status;
    }
}