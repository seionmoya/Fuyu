using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Trading
{
	[DataContract]
	public class LoyaltyInfo
	{
		[DataMember(Name = "minLevel")]
		public int MinProfileLevel { get; set; }

		[DataMember(Name = "minSalesSum")]
		public long MinSalesSum { get; set; }

		[DataMember(Name = "minStanding")]
		public double MinStanding { get; set; }

		[DataMember(Name = "buy_price_coef")]
		public double SellToTraderPriceCoef { get; set; }

		[DataMember(Name = "repair_price_coef")]
		public double RepairPriceCoef { get; set; }

		[DataMember(Name = "insurance_price_coef")]
		public double InsurancePriceCoef { get; set; }

		[DataMember(Name = "exchange_price_coef")]
		public double ExchangePriceCoef { get; set; }

		[DataMember(Name = "heal_price_coef")]
		public float HealPriceCoef { get; set; }
	}
}
