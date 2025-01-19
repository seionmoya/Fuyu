using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading;

[DataContract]
public class TradingItemScheme
{
    [DataMember(Name = "id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "scheme_id")]
    public int SchemeId { get; set; }
}