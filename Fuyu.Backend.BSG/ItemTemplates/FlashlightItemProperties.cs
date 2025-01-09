using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class FlashlightItemProperties : FunctionalModItemProperties
{
    [DataMember(Name = "ModesCount")]
    public int ModesCount { get; set; }
}