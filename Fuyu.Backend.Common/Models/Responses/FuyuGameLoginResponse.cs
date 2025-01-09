using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Responses;

[DataContract]
public class FuyuGameLoginResponse
{
    [DataMember]
    public string SessionId;
}