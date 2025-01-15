using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading;

[DataContract]
public class BarterComponent
{
    [DataMember(Name = "_tpl")]
    public MongoId Template { get; set; }

    [DataMember(Name = "count")]
    public double Count { get; set; }

    [DataMember(Name = "level", EmitDefaultValue = false)]
    public int? Level { get; set; }

    [DataMember(Name = "side", EmitDefaultValue = false)]
    public EDogtagExchangeSide? Side { get; set; }

    [DataMember(Name = "onlyFunctional", EmitDefaultValue = false)]
    public bool? OnlyFunctional { get; set; }
}