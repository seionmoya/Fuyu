using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Responses;

[DataContract]
public class AccountRegisterGameResponse
{
    [DataMember]
    public int AccountId { get; set; }
}