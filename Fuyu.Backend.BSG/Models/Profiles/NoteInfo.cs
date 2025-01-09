using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class NotesInfo
{
    [DataMember(Name = "Notes")]
    public List<Note> Notes { get; set; }
}