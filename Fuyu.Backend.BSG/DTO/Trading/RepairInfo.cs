using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.DTO.Trading
{
	[DataContract]
	public class RepairInfo
	{
		[DataMember(Name = "quality")]
		public float Quality { get; set; }

		[DataMember(Name = "excluded_category")]
		public MongoId[] ExcludedCategories { get; set; }

		[DataMember(Name = "currency")]
		public string Currency { get; set; }

		[DataMember(Name = "currency_coefficient")]
		public float CurrencyCoefficient { get; set; }

		[DataMember(Name = "availability")]
		public bool Availability { get; set; }
	}
}
