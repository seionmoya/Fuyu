using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
	[DataContract]
	public class ChargeItemProperties : GearModItemProperties
	{
		[DataMember(Name = "DurabilityBurnModificator")]
		public float DurabilityBurnModificator { get; set; } = 1f;
	}
}
