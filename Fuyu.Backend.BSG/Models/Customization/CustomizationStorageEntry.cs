using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Customization
{
    [DataContract]
    public class CustomizationStorageEntry
    {
        [DataMember]
        public MongoId id;

        [DataMember]
        public string source;

        // TODO: enum
        // seionmoya, 2025-01-04
        [DataMember]
        public string type;
    }
}