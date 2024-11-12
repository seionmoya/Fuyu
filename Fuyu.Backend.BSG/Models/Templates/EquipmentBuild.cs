using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;

namespace Fuyu.Backend.BSG.Models.Templates
{
    [DataContract]
    public class EquipmentBuild
    {
        [DataMember]
        public string Id;

        [DataMember]
        public string Name;

        [DataMember]
        public ItemInstance[] Items;

        [DataMember]
        public string Root;

        [DataMember]
        public string BuildType;

        [DataMember]
        public EEquipmentBuildType type;
    }
}