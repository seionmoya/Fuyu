using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class MaxItemCount
    {
        [DataMember]
        public string TemplateId;

        [DataMember]
        public int Value;
    }
}