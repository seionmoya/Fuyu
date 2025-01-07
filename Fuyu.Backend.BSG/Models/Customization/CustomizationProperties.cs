using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Customization
{
    [DataContract]
    public class CustomizationProperties
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string[] Side { get; set; }

        [DataMember]
        public string BodyPart { get; set; }

        [DataMember]
        public object Prefab { get; set; }   // can be String or BundleAddress

        [DataMember]
        public ResourceKey WatchPrefab { get; set; }

        [DataMember]
        public bool IntegratedArmorVest { get; set; }

        [DataMember]
        public Vector3 WatchPosition { get; set; }

        [DataMember]
        public Vector3 WatchRotation { get; set; }

        [DataMember]
        public bool AvailableAsDefault { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string Hands { get; set; }

        [DataMember]
        public string Feet { get; set; }
    }
}