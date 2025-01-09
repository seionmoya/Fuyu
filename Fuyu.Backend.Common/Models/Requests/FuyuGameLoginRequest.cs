using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Requests;

[DataContract]
public class FuyuGameLoginRequest
{
    [DataMember]
    public int AccountId;
}