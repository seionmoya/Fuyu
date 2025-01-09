using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemTagComponent
{
    [DataMember]
    public int Color { get; set; }

    [DataMember]
    public string Name { get; set; }
}