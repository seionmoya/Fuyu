using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Info
{
    [DataContract]
    public class Ban
    {
        [DataMember]
        public EBanType banType;

        [DataMember]
        public long dateTime;
    }
}