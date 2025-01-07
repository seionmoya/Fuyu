using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class Banner
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public ResourceKey pic { get; set; }
    }
}