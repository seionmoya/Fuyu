using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemInfo
{
    [DataMember(Name = "Id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "Items")]
    public ItemInstance[] Items { get; set; }
}