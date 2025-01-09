using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class InsuranceCostRequest
{
    [DataMember(Name = "traders")]
    public MongoId[] Traders { get; set; }

    [DataMember(Name = "items")]
    public MongoId[] ItemIds { get; set; }
}