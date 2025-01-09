using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Survey;

[DataContract]
public class QuestionAnswer
{
    [DataMember(Name = "questionId")]
    public int QuestionId { get; set; }

    [DataMember(Name = "answerType")]
    public EAnswerType AnswerType { get; set; }

    // TODO: Proper type.
    // Can be: int? | string | List<int>
    [DataMember(Name = "answers")]
    public object answers { get; set; }
}
