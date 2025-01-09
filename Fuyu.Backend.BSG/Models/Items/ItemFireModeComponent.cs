using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Common;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemFireModeComponent
{
    [DataMember]
    public EFireMode FireMode { get; set; }
}