using System.Runtime.Serialization;
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
        public string[] Suites;

        [DataMember]
        public bool ShouldWipe;
    }
}