using System.Runtime.Serialization;
using Fuyu.Launcher.Common.Models.Messages;

namespace Fuyu.Launcher.Core.Models.Messages;

[DataContract]
public class NavigateGameMessage : Message
{
    [DataMember(Name = "game")]
    public string Game { get; set; }
}