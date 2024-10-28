using System.Collections.Generic;
using System.Linq;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Services
{
	public static class InventoryService
	{
		public static List<ItemInstance> RemoveItem(InventoryInfo inventory, ItemInstance item)
		{
			var itemsToRemove = ItemService.GetItemAndChildren(inventory.items, item);
			// TODO: Change this later, I would rather itemsToRemove be List<ItemInstance>
			// -- nexus4880, 2024-10-26
			var itemInstances = inventory.items.Where(i => itemsToRemove.Contains(i._id)).ToList();

			inventory.items.RemoveAll(i => itemsToRemove.Contains(i._id));

			return itemInstances;
		}

		// NOTE:
		// * order is really important here!
		// -- seionmoya, 2024-10-24
		public static void RegenerateIds(InventoryInfo inventory)
		{
			var mapping = new Dictionary<MongoId, MongoId>();

			// regenerate inventory equipment
			mapping.Add(inventory.equipment, new MongoId(true));
			inventory.equipment = mapping[inventory.equipment];

			// regenerate inventory stash
			if (inventory.stash != null)
			{
				mapping.Add(inventory.stash.Value, new MongoId(true));
				inventory.stash = mapping[inventory.stash.Value];
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
			if (inventory.items != null)
			{
				foreach (var item in inventory.items)
				{
					if (!mapping.ContainsKey(item._id))
					{
						mapping.Add(item._id, new MongoId(true));
					}
				}

				ItemService.RegenerateItemIds(inventory.items, mapping);
			}

			// regenerate inventory fastpanel
			if (inventory.fastPanel != null)
			{
				foreach (var kvp in inventory.fastPanel)
				{
					inventory.fastPanel[kvp.Key] = mapping[kvp.Value];
				}
			}

			// regenerate inventory favorite items
			if (inventory.favoriteItems != null)
			{
				for (var i = 0; i < inventory.favoriteItems.Length; ++i)
				{
					var itemId = inventory.favoriteItems[i];
					inventory.favoriteItems[i] = mapping[itemId];
				}
			}
		}
	}
}
