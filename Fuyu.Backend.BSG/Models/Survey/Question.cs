using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class Question
{
    [DataMember(Name = "id")]
    public int Id { get; set; }

    [DataMember(Name = "sort")]
    public int Sort { get; set; }

    [DataMember(Name = "titleLocaleKey")]
    public string TitleLocaleKey { get; set; }

    [DataMember(Name = "hintLocaleKey")]
    public string HintLocaleKey { get; set; }

    [DataMember(Name = "answerLimit")]
    public int AnswerLimit { get; set; }

    [DataMember(Name = "answerType")]
    public EAnswerType AnswerType { get; set; }

    [DataMember(Name = "answers")]
    public Answer[] Answers { get; set; }
}
