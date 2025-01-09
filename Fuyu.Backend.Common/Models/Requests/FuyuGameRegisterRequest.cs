using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Requests;

[DataContract]
public class FuyuGameRegisterRequest
{
    [DataMember]
    public string Username;

    [DataMember]
    public string Edition;
}