using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class KeyMechanicalItemProperties : KeyItemProperties
{
    [DataMember(Name = "MaximumNumberOfUsage")]
    public int MaximumNumberOfUsage { get; set; }
}