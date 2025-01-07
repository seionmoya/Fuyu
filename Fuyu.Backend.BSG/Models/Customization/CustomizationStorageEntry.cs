using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Customization
{
    [DataContract]
    public class CustomizationStorageEntry
    {
        [DataMember]
        public MongoId id { get; set; }

        [DataMember]
        public string source { get; set; }

        // TODO: enum
        // seionmoya, 2025-01-04
        [DataMember]
        public string type { get; set; }
    }
}