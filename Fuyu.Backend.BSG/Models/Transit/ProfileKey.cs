using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class ProfileKey
{
    [DataMember(Name = "_id")]
    public string Id { get; set; }

    [DataMember(Name = "keyId")]
    public string KeyId { get; set; }

    [DataMember(Name = "isSold")]
    public bool IsSold { get; set; }
}