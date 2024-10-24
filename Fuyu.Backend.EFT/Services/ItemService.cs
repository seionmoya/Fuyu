using System.Collections.Generic;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.Services
{
    public class ItemService
    {
        public static void RegenerateItemIds(ItemInstance[] items, Dictionary<MongoId, MongoId> mapping)
        {
            // replace ids
            foreach (var item in items)
            {
                // replace item id
                item._id = mapping[item._id];

                // replace item's parent id
                if (item.parentId != null)
                {
                    item.parentId = mapping[item.parentId.Value];
                }
            }
        }

        public static void RegenerateItemIds(ItemInstance[] items)
        {
            var mapping = new Dictionary<MongoId, MongoId>();

            // find all old ids
            foreach (var item in items)
            {
                if (!mapping.ContainsKey(item._id))
                {
                    mapping.Add(item._id, new MongoId(true));
                }
            }

            RegenerateItemIds(items, mapping);
        }

        // NOTE:
        // * order is really important here!
        // -- seionmoya, 2024-10-24
        public static void RegenerateInventoryIds(InventoryInfo inventory)
        {
            var mapping = new Dictionary<MongoId, MongoId>();

            // regenerate inventory equipment
            mapping.Add(inventory.equipment, new MongoId(true));
            inventory.equipment = mapping[inventory.equipment];

            // regenerate inventory stash
            if (inventory.stash != null)
            {
                mapping.Add(inventory.stash.Value, new MongoId(true));
                inventory.equipment = mapping[inventory.stash.Value];
            }

            // regenerate inventory quest raid items
            if (inventory.questRaidItems != null)
            {
                mapping.Add(inventory.questRaidItems.Value, new MongoId(true));
                inventory.questRaidItems = mapping[inventory.questRaidItems.Value];
            }

            // regenerate inventory quest stash items
            if (inventory.questStashItems != null)
            {
                mapping.Add(inventory.questStashItems.Value, new MongoId(true));
                inventory.questStashItems = mapping[inventory.questStashItems.Value];
            }

            // regenerate inventory sorting table
            if (inventory.sortingTable != null)
            {
                mapping.Add(inventory.sortingTable.Value, new MongoId(true));
                inventory.sortingTable = mapping[inventory.sortingTable.Value];
            }

            // regenerate inventory items
            foreach (var item in inventory.items)
            {
                if (!mapping.ContainsKey(item._id))
                {
                    mapping.Add(item._id, new MongoId(true));
                }
            }

            RegenerateItemIds(inventory.items, mapping);

            // regenerate inventory fastpanel
            foreach (var kvp in inventory.fastPanel)
            {
                inventory.fastPanel[kvp.Key] = mapping[kvp.Value];
            }

            // regenerate inventory favorite items
            for (var i = 0; i < inventory.favoriteItems.Length; ++i)
            {
                var itemId = inventory.favoriteItems[i];
                inventory.favoriteItems[i] = mapping[itemId];
            }
        }
    }
}