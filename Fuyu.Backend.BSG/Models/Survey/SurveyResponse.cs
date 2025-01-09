using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class SurveyResponse
{
    [DataMember(Name = "locale")]
    public Dictionary<string, Dictionary<string, string>> Localization { get; set; }

    [DataMember(Name = "survey")]
    public SurveyTemplate Template { get; set; }
}
