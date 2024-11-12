using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
	[DataContract]
	public class TagItemEvent : BaseItemEvent
	{
		[DataMember(Name = "item")]
		public MongoId Item { get; set; }

		[DataMember(Name = "TagName")]
		public string Name { get; set; }

		// TODO: proper type (TaxonomyColor)
		[DataMember(Name = "TagColor")]
		public int Color { get; set; }
	}
}
