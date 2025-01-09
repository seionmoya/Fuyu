using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Responses
{
    [DataContract]
    public class AccountGamesResponse
    {
        [DataMember]
        public Dictionary<string, int?> Games { get; set; }
    }
}