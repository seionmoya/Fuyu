using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class WelcomePageData
{
    [DataMember(Name = "titleLocaleKey")]
    public string TitleLocaleKey { get; set; }

    [DataMember(Name = "timeLocaleKey")]
    public string TimeLocaleKey { get; set; }

    [DataMember(Name = "descriptionLocaleKey")]
    public string DescriptionLocaleKey { get; set; }
}
