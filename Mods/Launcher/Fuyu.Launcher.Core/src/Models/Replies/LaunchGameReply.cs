using System.Runtime.Serialization;
using Fuyu.Launcher.Common.Models.Replies;

namespace Fuyu.Launcher.Core.Models.Replies;

[DataContract]
public class LaunchGameReply : Reply
{
    [DataMember(Name = "error")]
    public string Message { get; set; }
}