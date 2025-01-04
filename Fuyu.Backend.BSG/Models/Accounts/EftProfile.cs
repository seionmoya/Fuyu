using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Accounts
{
    [DataContract]
    public class EftProfile
    {
        [DataMember]
        public Profile Pmc;

        [DataMember]
        public Profile Savage;

        [DataMember]
        public CustomizationStorageEntry[] Customization;

        [DataMember]
        public bool ShouldWipe;
    }
}