using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemLightComponent
{
    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public int SelectedMode { get; set; }
}