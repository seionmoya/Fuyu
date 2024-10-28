using System.Collections.Generic;
using System.Linq;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Services
{
    public class ItemService
    {
        public static void RegenerateItemIds(IEnumerable<ItemInstance> items, Dictionary<MongoId, MongoId> mapping)
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

        public static void RegenerateItemIds(List<ItemInstance> items)
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

        public static List<MongoId> GetItemAndChildren(List<ItemInstance> items, ItemInstance item)
        {
            return GetItemAndChildren(items, item._id);
        }

        public static List<MongoId> GetItemAndChildren(List<ItemInstance> items, MongoId id)
        {
			List<MongoId> itemsToRemove = [id];
			bool foundNewItem = true;
			while (foundNewItem)
			{
				foundNewItem = false;
				var found = items.Where(
                    i => !itemsToRemove.Contains(i._id)
                    && i.parentId.HasValue
                    && itemsToRemove.Contains(i.parentId.Value));

				if (found.Any())
				{
					foundNewItem = true;
					itemsToRemove.AddRange(found.Select(i => i._id));
				}
			}

            return itemsToRemove;
		}
    }
}