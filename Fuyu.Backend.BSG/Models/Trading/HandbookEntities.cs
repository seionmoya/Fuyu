using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading
{
	[DataContract]
	public class HandbookEntities
	{
		[DataMember(Name = "id_list")]
		public MongoId[] Items { get; set; }

		[DataMember(Name = "category")]
		public MongoId[] Categories { get; set; }
	}
}
