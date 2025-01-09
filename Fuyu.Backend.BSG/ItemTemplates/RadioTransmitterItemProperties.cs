using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class RadioTransmitterItemProperties : SpecItemItemProperties
    {
        [DataMember(Name = "IsEncoded")]
        public bool IsEncoded { get; set; }
    }
}