using System.Runtime.Serialization;

namespace Fuyu.Launcher.Common.Models.Messages;

[DataContract]
public class Message
{
    [DataMember(Name = "type")]
    public string Type { get; set; }
}