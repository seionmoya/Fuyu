using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemEvents.Models
{
    [DataContract]
    public class ProfileChange
    {
        public ProfileChange()
        {
            UnlockedRecipes = [];
            Items = new ItemChanges();
        }

        [DataMember(Name = "experience")]
        public int Experience { get; set; }

        [DataMember(Name = "recipeUnlocked")]
        public Dictionary<MongoId, bool> UnlockedRecipes { get; set; }

        [DataMember(Name = "items")]
        public ItemChanges Items { get; set; }
    }
}
