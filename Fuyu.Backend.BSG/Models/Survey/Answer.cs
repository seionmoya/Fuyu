using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class Answer
{
    [DataMember(Name = "id")]
    public int Id;

    [DataMember(Name = "questionId")]
    public int QuestionId;

    [DataMember(Name = "sort")]
    public int SortIndex;

    [DataMember(Name = "localeKey")]
    public string LocaleKey;
}