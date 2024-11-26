using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class ExamineItemEvent : BaseItemEvent
    {
        [DataMember(Name = "item")]
        public MongoId TemplateId { get; set; }
    }
}
