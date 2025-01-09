using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Accounts;

[DataContract]
public class WipeProfile
{
    [DataMember]
    public Profile Profile { get; set; }

    [DataMember]
    public CustomizationStorageEntry[] Customization { get; set; }
}