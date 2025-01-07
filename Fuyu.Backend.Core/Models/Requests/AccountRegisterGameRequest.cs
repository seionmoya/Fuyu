using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Requests
{
    [DataContract]
    public class AccountRegisterGameRequest
    {
        [DataMember]
        public string Game { get; set; }

        [DataMember]
        public string Edition { get; set; }
    }
}