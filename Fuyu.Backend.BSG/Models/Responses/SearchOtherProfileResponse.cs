using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class SearchOtherProfileResponse
{
    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "aid")]
    public int AccountId { get; set; }

    [DataMember(Name = "info")]
    public OtherProfileInfo Info { get; set; }
}