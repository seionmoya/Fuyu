using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Profiles.Hideout;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class HideoutInfo
{
    // TODO: proper type
    [DataMember]
    public object Production { get; set; }

    [DataMember]
    public HideoutAreaInfo[] Areas { get; set; }

    // TODO: proper type
    [DataMember]
    public object Improvements { get; set; }

    [DataMember]
    public long Seed { get; set; }

    [DataMember(Name = "Customization")]
    public Dictionary<EHideoutCustomizationType, MongoId?> GlobalCustomization { get; set; }

    [DataMember]
    public Union<Dictionary<string, MongoId>, object[]> MannequinPoses { get; set; }
}