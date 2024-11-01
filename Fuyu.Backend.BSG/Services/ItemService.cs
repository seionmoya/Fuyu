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
                item.Id = mapping[item.Id];

                // replace item's parent id
                if (item.ParentId != null)
                {
                    item.ParentId = mapping[item.ParentId.Value];
                }
            }
        }

        public static void RegenerateItemIds(List<ItemInstance> items)
        {
            var mapping = new Dictionary<MongoId, MongoId>();

            // find all old ids
            foreach (var item in items)
            {
                if (!mapping.ContainsKey(item.Id))
                {
                    mapping.Add(item.Id, new MongoId(true));
                }
            }

            RegenerateItemIds(items, mapping);
        }

        public static List<ItemInstance> GetItemAndChildren(List<ItemInstance> items, ItemInstance item)
        {
            return GetItemAndChildren(items, item.Id);
        }

        public static List<ItemInstance> GetItemAndChildren(List<ItemInstance> items, MongoId id)
        {
			List<MongoId> idsToReturn = [id];
			bool foundNewItem = true;
			while (foundNewItem)
			{
				foundNewItem = false;
				var found = items.Where(
                    i => !idsToReturn.Contains(i.Id)
                    && i.ParentId.HasValue
                    && idsToReturn.Contains(i.ParentId.Value));

				if (found.Any())
				{
					foundNewItem = true;
					idsToReturn.AddRange(found.Select(i => i.Id));
				}
			}

            return items.Where(i => idsToReturn.Contains(i.Id)).ToList();
		}
    }
}