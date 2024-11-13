using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemCultistAmuletComponent
    {
        [DataMember(Name = "NumberOfUsages")]
        public int NumberOfUsages { get; set; }
    }
}