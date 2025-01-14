using System;
using System.Runtime.Serialization;

namespace Fuyu.Common.Models.States;

[DataContract]
public class State
{
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "type")]
    public Type Type { get; set; }

    [DataMember(Name = "value")]
    public object Value { get; set; }
}