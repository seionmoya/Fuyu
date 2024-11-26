using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
	[DataContract]
	public class MoneyItemProperties : StackableItemItemProperties
	{
		[DataMember(Name = "type")]
		public ECurrencyType type;

		[DataMember(Name = "eqMin")]
		public int eqMin;

		[DataMember(Name = "eqMax")]
		public int eqMax;

		[DataMember(Name = "rate")]
		public float rate;

		[DataMember(Name = "IsRagfairCurrency")]
		public bool IsRagfairCurrency;
	}

	public enum ECurrencyType
	{
		RUB,
		USD,
		EUR,
		GP
	}
}
