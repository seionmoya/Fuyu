using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Stats;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class StatsInfo
    {
        [DataMember]
        public EftStats Eft { get; set; }
    }
}