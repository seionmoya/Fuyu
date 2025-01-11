using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class MagazineBuildSaveRequest
{
    [DataMember]
    public MongoId Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Caliber { get; set; }

    [DataMember]
    public MagazineItem[] Items { get; set; }

    [DataMember]
    public int TopCount { get; set; }

    [DataMember]
    public int BottomCount { get; set; }
}