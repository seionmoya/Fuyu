using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
	[DataContract]
	public class MedsItemProperties : ItemProperties
	{
		[DataMember(Name = "medUseTime")]
		public float UseTime;

		[DataMember(Name = "bodyPartTimeMults")]
		public KeyValuePair<EBodyPart, float>[] BodyPartTimeMults;

		[DataMember(Name = "effects_health")]
		public object HealthEffects;

		[DataMember(Name = "effects_damage")]
		public object DamageEffects;

		[DataMember(Name = "StimulatorBuffs")]
		public string StimulatorBuffs;

		[DataMember(Name = "MaxHpResource")]
		public int MaxHpResource;

		[DataMember(Name = "hpResourceRate")]
		public float HpResourceRate;
	}
}
