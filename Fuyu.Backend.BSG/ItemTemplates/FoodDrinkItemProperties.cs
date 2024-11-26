using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class FoodDrinkItemProperties : ItemProperties
    {
        [DataMember(Name = "MaxResource")]
        public float MaxResource { get; set; }

        [DataMember(Name = "foodUseTime")]
        public int UseTime { get; set; }

        [DataMember(Name = "effects_health")]
        public object HealthEffects { get; set; }

        [DataMember(Name = "effects_damage")]
        public object DamageEffects { get; set; }

        [DataMember(Name = "StimulatorBuffs")]
        public string StimulatorBuffs { get; set; }
    }
}
