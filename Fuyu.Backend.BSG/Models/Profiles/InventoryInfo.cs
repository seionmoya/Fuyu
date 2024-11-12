using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class InventoryInfo
    {
        [DataMember(Name = "items")]
        public List<ItemInstance> Items { get; set; }

        [DataMember(Name = "equipment")]
        public MongoId Equipment { get; set; }

		[DataMember(Name = "stash")]
        public MongoId? Stash { get; set; }

		[DataMember(Name = "sortingTable")]
        public MongoId? SortingTable { get; set; }

		[DataMember(Name = "questRaidItems")]
        public MongoId? QuestRaidItems { get; set; }

		[DataMember(Name = "questStashItems")]
        public MongoId? QuestStashItems { get; set; }

		[DataMember(Name = "fastPanel")]
        public Dictionary<string, MongoId> FastPanel { get; set; }

		[DataMember(Name = "hideoutAreaStashes")]
        public Dictionary<string, MongoId> HideoutAreaStashes { get; set; }

		[DataMember(Name = "favoriteItems")]
        public MongoId[] FavoriteItems { get; set; }

		public List<ItemInstance> RemoveItem(ItemInstance item)
		{
			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> RemoveItem(MongoId id)
		{
            var item = Items.Find(i => i.Id == id);
			if (item == null)
            {
                throw new Exception($"Failed to find item with id {id} in inventory");
            }

			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> GetItemsByTemplate(MongoId tpl)
        {
            return Items.Where(i => i.TemplateId == tpl).ToList();
        }
    }
}