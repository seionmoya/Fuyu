using System.Runtime.Serialization;
using Fuyu.Launcher.Common.Models.Messages;

namespace Fuyu.Launcher.Core.Models.Messages;

[DataContract]
public class LoginAccountMessage : Message
{
    [DataMember(Name = "username")]
    public string Username { get; set; }

    [DataMember(Name = "password")]
    public string Password { get; set; }
}