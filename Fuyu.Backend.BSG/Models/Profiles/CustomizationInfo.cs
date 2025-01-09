using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class CustomizationInfo
{
    [DataMember]
    public MongoId Head { get; set; }

    [DataMember]
    public MongoId Body { get; set; }

    [DataMember]
    public MongoId Feet { get; set; }

    [DataMember]
    public MongoId Hands { get; set; }
}