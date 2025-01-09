using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.Core.Models.Accounts;

[DataContract]
public class Account
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Username { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public Dictionary<string, int?> Games { get; set; }

    [DataMember]
    public bool IsBanned { get; set; }
}