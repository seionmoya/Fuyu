using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class FarewellPageData
{
    [DataMember(Name = "textLocaleKey")]
    public string TextLocaleKey { get; set; }
}
