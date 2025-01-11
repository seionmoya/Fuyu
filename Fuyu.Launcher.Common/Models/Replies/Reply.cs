using System.Runtime.Serialization;

namespace Fuyu.Launcher.Common.Models.Replies;

[DataContract]
public class AbstractReply
{
    [DataMember(Name = "type")]
    public string Type { get; set; }
}