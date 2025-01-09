using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class MaxItemCount
{
    [DataMember]
    public string TemplateId { get; set; }

    [DataMember]
    public int Value { get; set; }
}