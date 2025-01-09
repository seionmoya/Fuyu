using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class LootItem
{
    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public Vector3 Position{ get; set; }

    [DataMember]
    public Vector3 Rotation { get; set; }

    // TODO: Use proper Type
    [DataMember]
    public object Item { get; set; }

    [DataMember]
    public MongoId[] ValidProfiles { get; set; }

    [DataMember]
    public bool IsContainer { get; set; }

    [DataMember]
    public bool useGravity { get; set; }

    [DataMember]
    public bool randomRotation { get; set; }

    [DataMember]
    public Vector3 Shift { get; set; }

    [DataMember]
    public short PlatformId { get; set; }
}
