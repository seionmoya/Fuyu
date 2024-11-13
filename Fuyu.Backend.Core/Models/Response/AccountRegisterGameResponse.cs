using System.Runtime.Serialization;
using Fuyu.Backend.Core.Models.Accounts;

namespace Fuyu.Backend.Core.Models.Responses
{
    [DataContract]
    public class AccountRegisterGameResponse
    {
        [DataMember]
        public ERegisterStatus Status;

        [DataMember]
        public int AccountId;
    }
}