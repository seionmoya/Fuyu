using System.Runtime.Serialization;

namespace Fuyu.Launcher.Common.Models.Messages;

[DataContract]
public abstract class AbstractMessage
{
    [DataMember(Name = "type")]
    public string Type { get; set; }
}