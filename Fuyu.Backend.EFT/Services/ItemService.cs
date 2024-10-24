using System.Collections.Generic;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.Services
{
    public class ItemService
    {
        public static ItemInstance[] RegenerateItemIds(ItemInstance[] items)
        {
            var newIds = new Dictionary<MongoId, MongoId>();

            // find all old ids
            foreach (var item in items)
            {
                if (!newIds.ContainsKey(item._id))
                {
                    newIds.Add(item._id, new MongoId(true));
                }
            }

            // replace ids
            foreach (var item in items)
            {
                // replace item id
                item._id = newIds[item._id];

                // replace item's parent id
                if (item.parentId != null)
                {
                    item.parentId = newIds[item.parentId.Value];
                }
            }

            return items;
        }
    }
}