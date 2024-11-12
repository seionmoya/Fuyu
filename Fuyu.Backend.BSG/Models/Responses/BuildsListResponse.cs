using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Templates;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class BuildsListResponse
    {
        [DataMember]
        public EquipmentBuild[] equipmentBuild;

        // TODO: proper type
        [DataMember]
        public object[] weaponBuilds;

        // TODO: proper type
        [DataMember]
        public object[] magazineBuilds;
    }
}