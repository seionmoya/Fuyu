using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class CustomizationStorageResponse
    {
        [DataMember]
        public string _id;

        [DataMember]
        public string[] suites;
    }
}