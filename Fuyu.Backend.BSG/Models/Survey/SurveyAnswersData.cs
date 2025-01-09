using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class SurveyAnswersData
{
    [DataMember(Name = "surveyId")]
    public int SurveyId { get; set; }

    [DataMember(Name = "answers")]
    public List<QuestionAnswer> Answers { get; set; }
}
