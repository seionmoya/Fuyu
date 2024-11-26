using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class WeaponBuild
    {
        [DataMember(Name = "Id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "ItemIconName")]
        public string ItemIconName { get; set; }

        [DataMember(Name = "HandbookName")]
        public string HandbookName { get; set; }

        [DataMember(Name = "FromPreset")]
        public bool FromPreset { get; set; }
    }
}