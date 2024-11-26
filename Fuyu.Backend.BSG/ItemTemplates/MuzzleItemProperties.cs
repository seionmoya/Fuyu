using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
	[DataContract]
	public class MuzzleItemProperties : FunctionalModItemProperties
	{
		[DataMember(Name = "muzzleModType")]
		public EMuzzleType muzzleModType;

		[DataMember(Name = "DurabilityBurnModificator")]
		public float DurabilityBurnModificator = 1f;

		[DataMember(Name = "HeatFactor")]
		public float HeatFactor = 1f;

		[DataMember(Name = "CoolFactor")]
		public float CoolFactor = 1f;
	}

	public enum EMuzzleType
	{
		silencer,
		conpensator,
		pms,
		brake,
		muzzleCombo
	}
}
