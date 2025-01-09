using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class HandguardItemProperties : MasterModItemProperties
    {
        [DataMember(Name = "HeatFactor")]
        public float HeatFactor = 1f;

        [DataMember(Name = "CoolFactor")]
        public float CoolFactor = 1f;
    }
}