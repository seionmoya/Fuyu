using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.DTO.Profiles
{
    [DataContract]
    public class InventoryInfo
    {
        [DataMember]
        public List<ItemInstance> items;

        [DataMember]
        public MongoId equipment;

        [DataMember]
        public MongoId? stash;

        [DataMember]
        public MongoId? sortingTable;

        [DataMember]
        public MongoId? questRaidItems;

        [DataMember]
        public MongoId? questStashItems;

        [DataMember]
        public Dictionary<string, MongoId> fastPanel;

        [DataMember]
        public Dictionary<string, MongoId> hideoutAreaStashes;

        [DataMember]
        public MongoId[] favoriteItems;

        public List<ItemInstance> RemoveItem(ItemInstance item)
		{
			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> RemoveItem(MongoId id)
		{
            var item = items.Find(i => i._id == id);
			if (item == null)
            {
                throw new Exception($"Failed to find item with id {id} in inventory");
            }

			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> GetItemsByTemplate(MongoId tpl)
        {
            return items.Where(i => i._tpl == tpl).ToList();
        }
    }
}