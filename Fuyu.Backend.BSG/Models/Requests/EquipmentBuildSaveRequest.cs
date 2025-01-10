using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class EquipmentBuildSaveRequest
{
    [DataMember]
    public MongoId Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Root { get; set; }

    [DataMember]
    public ItemInstance[] Items { get; set; }
}
