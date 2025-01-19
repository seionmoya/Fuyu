using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class MagazineCartridge
{
    [DataMember(Name = "_name")]
    public string Name { get; set; }

    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "_parent")]
    public MongoId Parent { get; set; }

    [DataMember(Name = "max_count")]
    public int MaxCount { get; set; }

    [DataMember(Name = "_props")]
    public SlotProperties Properties { get; set; }
}