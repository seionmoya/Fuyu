using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Config;

[DataContract]
public class MainConfig
{
    [DataMember]
    public WebConfig WebConfig { get; set; } = new();
}
