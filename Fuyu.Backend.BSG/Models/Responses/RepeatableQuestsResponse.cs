using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class RepeatableQuestsResponse
{
    // TODO: proper type
    [DataMember]
    public object[] squad { get; set; }
}