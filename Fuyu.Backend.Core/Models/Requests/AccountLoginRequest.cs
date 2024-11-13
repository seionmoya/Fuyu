using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Requests
{
    [DataContract]
    public class AccountLoginRequest
    {
        [DataMember]
        public string Username;

        [DataMember]
        public string Password;
    }
}