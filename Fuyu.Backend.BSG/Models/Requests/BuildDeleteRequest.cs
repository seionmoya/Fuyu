using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class BuildDeleteRequest
{
    [DataMember]
    public MongoId Id { get; set; }
}