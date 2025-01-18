using System;
using System.Collections.Generic;
using System.Linq;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Services;

public class ItemService
{
    public static ItemService Instance => instance.Value;
    private static readonly Lazy<ItemService> instance = new(() => new ItemService());

    private readonly ItemFactoryService _itemFactoryService;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private ItemService()
    {
        _itemFactoryService = ItemFactoryService.Instance;
    }

    public void RegenerateItemIds(IEnumerable<ItemInstance> items, Dictionary<string, string> mapping)
    {
        // replace ids
        foreach (var item in items)
        {
            // replace item id
            item.Id = mapping[item.Id];

            // replace item's parent id
            if (item.ParentId != null && mapping.TryGetValue(item.ParentId, out var parentId))
            {
                item.ParentId = parentId;
            }
        }
    }

    public void RegenerateItemIds(List<ItemInstance> items)
    {
        var mapping = new Dictionary<string, string>();
        foreach (var item in items)
        {
            mapping.TryAdd(item.Id, new MongoId(true));
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

    public List<ItemInstance> GetItemAndChildren(List<ItemInstance> items, MongoId id)
    {
        var idsToReturn = new List<string> { id };
        var keepSearching = true;

        while (keepSearching)
        {
            keepSearching = false;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (!idsToReturn.Contains(item.Id) && idsToReturn.Contains(item.ParentId))
                {
                    idsToReturn.Add(item.Id);
                    keepSearching = true;
                }
            }
        }

        return items.Where(i => idsToReturn.Contains(i.Id)).ToList();
    }

    /// <summary>
    /// Only an item and its children should be passed into this
    /// </summary>
    public (int width, int height) CalculateItemSize(List<ItemInstance> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (items.Count == 0)
        {
            throw new Exception($"{nameof(items)} is empty");
        }

        var root = items[0];

        if (root.Size != null)
        {
            return root.Size.Value;
        }

        var rootProperties = _itemFactoryService.GetItemProperties<ItemProperties>(root.TemplateId);

        var width = rootProperties.Width;
        var height = rootProperties.Height;

        var sizeUp = 0;
        var sizeDown = 0;
        var sizeLeft = 0;
        var sizeRight = 0;
        var forcedUp = 0;
        var forcedDown = 0;
        var forcedLeft = 0;
        var forcedRight = 0;

        for (var i = 1; i < items.Count; i++)
        {
            var itemProperties = _itemFactoryService.GetItemProperties<ItemProperties>(items[i].TemplateId);

            if (itemProperties == null)
            {
                continue;
            }

            if (itemProperties.ExtraSizeForceAdd)
            {
                forcedUp += itemProperties.ExtraSizeUp;
                forcedDown += itemProperties.ExtraSizeDown;
                forcedLeft += itemProperties.ExtraSizeLeft;
                forcedRight += itemProperties.ExtraSizeRight;
            }
            else
            {
                sizeUp = Math.Max(sizeUp, itemProperties.ExtraSizeUp);
                sizeDown = Math.Max(sizeDown, itemProperties.ExtraSizeDown);
                sizeLeft = Math.Max(sizeLeft, itemProperties.ExtraSizeLeft);
                sizeRight = Math.Max(sizeRight, itemProperties.ExtraSizeRight);
            }
        }

        width += sizeLeft + sizeRight + forcedLeft + forcedRight;
        height += sizeUp + sizeDown + forcedUp + forcedDown;

        if (root.Location.IsValue1 && root.Location.Value1 != null && root.Location.Value1.r == EItemRotation.Vertical)
        {
            root.Size = (height, width);
            return (height, width);
        }

        root.Size = (width, height);
        return (width, height);
    }

    public LocationInGrid GetNextFreeSlot(ItemInstance containerItem,
        List<ItemInstance> items, int width, int height, ref bool[] matrix, out string gridName,
        EItemRotation desiredRotation = EItemRotation.Horizontal)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        gridName = null;

        var containerItemProperties =
            _itemFactoryService.GetItemProperties<CompoundItemItemProperties>(containerItem.TemplateId);

        if (containerItemProperties?.Grids == null)
        {
            return null;
        }

        foreach (var grid in containerItemProperties.Grids)
        {
            gridName = grid.Name;
            var gridWidth = grid.Properties.CellsHorizontal;
            var gridHeight = grid.Properties.CellsVertical;

            // Check if the item is too big for this grid
            if (width > gridWidth || height > gridHeight)
            {
                continue;
            }

            if (matrix == null)
            {
                matrix = GenerateMatrix(gridWidth, gridHeight, items);
            }

            for (var y = 0; y <= gridHeight - height; y++)
            {
                for (var x = 0; x <= gridWidth - width; x++)
                {
                    var canFit = true;

                    for (var dy = 0; canFit && dy < height; dy++)
                    {
                        for (var dx = 0; dx < width; dx++)
                        {
                            if (matrix[(y + dy) * gridWidth + (x + dx)])
                            {
                                canFit = false;
                                break;
                            }
                        }
                    }

                    if (canFit)
                    {
                        return new LocationInGrid { x = x, y = y, r = desiredRotation };
                    }
                }
            }
        }

        return null;
    }

    public bool[] GenerateMatrix(int gridWidth, int gridHeight, List<ItemInstance> items)
    {
        var matrix = new bool[gridWidth * gridHeight];

        foreach (var itemInThisGrid in items)
        {
            if (!itemInThisGrid.Location.IsValue1 || itemInThisGrid.Location.Value1 == null)
            {
                continue;
            }

            var itemLocation = itemInThisGrid.Location.Value1;
            var itemAndChildren = GetItemAndChildren(items, itemInThisGrid);
            (int itemWidth, int itemHeight) = CalculateItemSize(itemAndChildren);

            if (itemLocation.x < 0 || itemLocation.y < 0 ||
                itemLocation.x + itemWidth > gridWidth ||
                itemLocation.y + itemHeight > gridHeight)
            {
                continue;
            }

            for (var y = 0; y < itemHeight; y++)
            {
                for (var x = 0; x < itemWidth; x++)
                {
                    var cellX = itemLocation.x + x;
                    var cellY = itemLocation.y + y;
                    matrix[cellY * gridWidth + cellX] = true;
                }
            }
        }

        return matrix;
    }
}