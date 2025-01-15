using System;
using System.Collections.Generic;
using System.Linq;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Services;

public class ItemService
{
    public static ItemService Instance => instance.Value;
    private static readonly Lazy<ItemService> instance = new(() => new ItemService());

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private ItemService()
    {

    }

    public void RegenerateItemIds(IEnumerable<ItemInstance> items, Dictionary<MongoId, MongoId> mapping)
    {
        // replace ids
        foreach (var item in items)
        {
            // replace item id
            item.Id = mapping[item.Id];

            // replace item's parent id
            if (item.ParentId != null)
            {
                item.ParentId = mapping[item.ParentId];
            }
        }
    }

    public void RegenerateItemIds(List<ItemInstance> items)
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

    public List<ItemInstance> GetItemAndChildren(List<ItemInstance> items, ItemInstance item)
    {
        return GetItemAndChildren(items, item.Id);
    }

    public bool IsChildItem(ItemInstance item, List<MongoId> ids)
    {
        if (item.ParentId == null)
        {
            return false;
        }

        return ids.FindIndex(i => i == item.ParentId) != -1;
    }

    // TODO: make the LINQ pattern more explicit
    public List<ItemInstance> GetItemAndChildren(List<ItemInstance> items, MongoId id)
    {
        var idsToReturn = new List<string>
        {
            id
        };

        start:
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            if (!idsToReturn.Contains(item.Id) && idsToReturn.Contains(item.ParentId))
            {
                idsToReturn.Add(item.Id);
                goto start;
            }
        }

        return items.Where(i => idsToReturn.Contains(i.Id)).ToList();
    }
}