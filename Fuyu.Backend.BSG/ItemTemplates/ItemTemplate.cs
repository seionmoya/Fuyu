using System.Runtime.Serialization;
using Fuyu.Common.Hashing;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class ItemTemplate
    {
        [DataMember(Name = "_type")]
        public ENodeType Type { get; set; }

        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "_name")]
        public string Name { get; set; }

        [DataMember(Name = "_parent")]
        public string ParentId { get; set; }

        [DataMember(Name = "_props")]
        public JObject Props { get; set; }

        [DataMember(Name = "_proto", EmitDefaultValue = false)]
        // NOTE: Could be MongoId?
        // -- nexus4880, 2024-10-18
        public string Proto { get; set; }
    }

    public enum ENodeType
    {
        Item,
        Preset,
        Node
    }
}