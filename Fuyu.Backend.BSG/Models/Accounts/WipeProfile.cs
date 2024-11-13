using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;

namespace Fuyu.Backend.BSG.Models.Accounts
{
    [DataContract]
    public class WipeProfile
    {
        [DataMember]
        public Profile Profile;

        [DataMember]
        public string[] Suites;
    }
}