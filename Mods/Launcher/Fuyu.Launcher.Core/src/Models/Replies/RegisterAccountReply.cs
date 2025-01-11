using System.Runtime.Serialization;
using Fuyu.Launcher.Common.Models.Replies;

namespace Fuyu.Launcher.Core.Models.Replies;

[DataContract]
public class RegisterAccountReply : Reply
{
    [DataMember(Name = "message")]
    public string Message { get; set; }
}