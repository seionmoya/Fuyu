using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Accounts
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public int Id;

        [DataMember]
        public string Username;

        [DataMember]
        public string Password;

        [DataMember]
        public Dictionary<string, int?> Games;

        [DataMember]
        public bool IsBanned;
    }
}