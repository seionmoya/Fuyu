using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class InsuredItem
{
    [DataMember]
    public MongoId tid { get; set; }

    [DataMember]
    public MongoId itemId { get; set; }
}