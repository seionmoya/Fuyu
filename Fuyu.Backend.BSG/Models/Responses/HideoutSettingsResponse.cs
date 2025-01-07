using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class HideoutSettingsResponse
    {
        [DataMember]
        public double generatorSpeedWithoutFuel { get; set; }

        [DataMember]
        public double generatorFuelFlowRate { get; set; }

        [DataMember]
        public double airFilterUnitFlowRate { get; set; }

        [DataMember]
        public double gpuBoostRate { get; set; }
    }
}