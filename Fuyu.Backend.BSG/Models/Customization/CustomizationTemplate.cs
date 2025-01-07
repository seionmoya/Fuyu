using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Customization
{
    [DataContract]
    public class CustomizationTemplate
    {
        [DataMember]
        public string _id { get; set; }

        [DataMember]
        public string _name { get; set; }

        [DataMember]
        public string _parent { get; set; }

        [DataMember]
        public string _type { get; set; }

        [DataMember]
        public CustomizationProperties _props { get; set; }

        [DataMember]
        public string _proto { get; set; }
    }
}