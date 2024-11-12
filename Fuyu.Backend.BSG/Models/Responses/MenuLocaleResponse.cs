using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class MenuLocaleResponse
    {
        [DataMember]
        public Dictionary<string, string> menu;
    }
}