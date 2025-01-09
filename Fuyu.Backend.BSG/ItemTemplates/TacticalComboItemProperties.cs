using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class TacticalComboItemProperties : FunctionalModItemProperties
    {
        [DataMember(Name = "ModesCount")]
        public int ModesCount { get; set; }
    }
}