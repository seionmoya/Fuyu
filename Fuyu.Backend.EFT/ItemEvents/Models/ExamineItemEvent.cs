using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemEvents.Models;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.ItemEvents.Models
{
	[DataContract]
	public class ExamineItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId TemplateId { get; set; }
	}
}
