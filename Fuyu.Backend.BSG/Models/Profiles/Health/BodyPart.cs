using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Profiles.Health
{
    [DataContract]
    public class BodyPart
    {
        [DataMember]
        public CurrentMaximum<float> Health;
    }
}