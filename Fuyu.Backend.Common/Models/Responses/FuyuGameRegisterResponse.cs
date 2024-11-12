using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Responses
{
    [DataContract]
    public class FuyuGameRegisterResponse
    {
        [DataMember]
        public int AccountId;
    }
}