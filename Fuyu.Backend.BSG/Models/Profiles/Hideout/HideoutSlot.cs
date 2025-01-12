using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;

namespace Fuyu.Backend.BSG.Models.Profiles.Hideout;

public class HideoutSlot
{
    [DataMember(Name = "item")]
    public ItemInstance[] Items { get; set; }
}