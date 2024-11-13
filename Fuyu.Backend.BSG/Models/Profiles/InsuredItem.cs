using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class InsuredItem
    {
        [DataMember]
        public MongoId tid;

        [DataMember]
        public MongoId itemId;
    }
}