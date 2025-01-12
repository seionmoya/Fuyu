using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Raid;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class PlayerVisualRepresentation
{
    [DataMember]
    public ChatMemberInfo Info { get; set; }

    [DataMember]
    public Dictionary<EBodyModelPart, MongoId> Customization { get; set; }

    [DataMember]
    public ItemInfo Equipment { get; set; }
}