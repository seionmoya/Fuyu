using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class CustomizationInfo
{
    [DataMember(Name = "head")]
    public MongoId Head { get; set; }

    [DataMember(Name = "body")]
    public MongoId Body { get; set; }

    [DataMember(Name = "feet")]
    public MongoId Feet { get; set; }

    [DataMember(Name = "hands")]
    public MongoId Hands { get; set; }

    [DataMember(Name = "dogtag", EmitDefaultValue = false)]
    public MongoId DogTag { get; set; }



    /*public Dictionary<EBodyModelPart, MongoId> ToDictionary()
    {
        return new Dictionary<EBodyModelPart, MongoId>
        {
            { EBodyModelPart.Head, Head },
            { EBodyModelPart.Body, Body },
            { EBodyModelPart.Feet, Feet },
            { EBodyModelPart.Hands, Hands },
            { EBodyModelPart.DogTag, DogTag }
        };
    }*/
}
