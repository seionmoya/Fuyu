using System.Runtime.Serialization;

namespace Fuyu.Launcher.Common.Models.Replies;

[DataContract]
public class Reply
{
    [DataMember(Name = "type")]
    public string Type { get; set; }
}