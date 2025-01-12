using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class GetOtherProfileRequest
{
    [DataMember(Name = "accountId")]
    public int AccountId { get; set; }
}