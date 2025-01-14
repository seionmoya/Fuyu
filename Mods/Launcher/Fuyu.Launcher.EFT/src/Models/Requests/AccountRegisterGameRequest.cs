using System.Runtime.Serialization;

namespace Fuyu.Launcher.EFT.Models.Requests;

[DataContract]
public class AccountRegisterGameRequest
{
    [DataMember]
    public string Game { get; set; }

    [DataMember]
    public string Edition { get; set; }
}