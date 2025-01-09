using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common;

[DataContract]
public class ResourceKey
{
    [DataMember(Name = "path")]
    public string Path { get; set; }

    [DataMember(Name = "rcid")]
    public string RCID { get; set; }
}