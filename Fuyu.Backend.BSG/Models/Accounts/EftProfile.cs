using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.BSG.Models.Accounts;

[DataContract]
public class EftProfile
{
    [DataMember]
    public Profile Pmc { get; set; }

    [DataMember]
    public Profile Savage { get; set; }

    [DataMember]
    public CustomizationStorageEntry[] Customization { get; set; }

    [DataMember]
    public BuildsListResponse Builds { get; set; }

    [DataMember]
    public bool ShouldWipe { get; set; }
}