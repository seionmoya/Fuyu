using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class LightLaserItemProperties : FunctionalModItemProperties
    {
        [DataMember(Name = "ModesCount")]
        public int ModesCount { get; set; }
    }
}
