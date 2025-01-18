using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class InventoryInfo
{
    public Dictionary<MongoId, ItemInstance> ItemsMap { get; private set; } = [];

    public List<ItemInstance> Items => ItemsMap.Values.ToList();

    [DataMember(Name = "items")]
    private List<ItemInstance> _itemsForSerialization;

    [OnSerializing]
    private void OnSerializing(StreamingContext _)
    {
        _itemsForSerialization = Items;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext _)
    {
        ItemsMap = _itemsForSerialization.ToDictionary(i => i.Id);
    }

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

    [DataMember(Name = "hideoutCustomizationStashId")]
    public MongoId? HideoutCustomizationStashId { get; set; }

    public LocationInGrid GetNextFreeSlot(ItemService itemService, int width, int height, out string gridName,
        EItemRotation desiredRotation = EItemRotation.Horizontal)
    {
        var result = itemService.GetNextFreeSlot(StashItem, Items, width, height, ref _matrix,
            out gridName, desiredRotation);
        return result;
    }

    public void EnsureMatrixGenerated(ItemService itemService, ItemFactoryService itemFactoryService, bool force = false)
    {
        if ((force || _matrix == null) && Stash.HasValue)
        {
            itemService ??= ItemService.Instance;
            itemFactoryService ??= ItemFactoryService.Instance;

            var stashItem = FindItem(Stash.Value);
            var template = itemFactoryService.ItemTemplates[stashItem.TemplateId];
            var compoundItemItemProperties = template.Props.ToObject<CompoundItemItemProperties>();
            var primaryGrid = compoundItemItemProperties.Grids[0].Properties;
            _matrix = itemService.GenerateMatrix(primaryGrid.CellsHorizontal, primaryGrid.CellsVertical,
                [.. Items]);
        }
    }

    public void AddItems(ItemService itemService, ItemFactoryService itemFactoryService, List<ItemInstance> items)
    {
        if (items.Count == 0)
        {
            throw new Exception($"{nameof(items)}.Count must be greater than 0");
        }

        if (!Stash.HasValue)
        {
            return;
        }

        var stashItem = FindItem(Stash.Value);
        var template = itemFactoryService.ItemTemplates[stashItem.TemplateId];
        var compoundItemItemProperties = template.Props.ToObject<CompoundItemItemProperties>();
        var primaryGrid = compoundItemItemProperties.Grids[0].Properties;

        var rootItem = items[0];

        if (!rootItem.Location.IsValue1)
        {
            throw new Exception("!rootItem.Location.IsValue1");
        }

        var location = rootItem.Location.Value1;

        if (location == null)
        {
            throw new Exception("Location is null");
        }

        var itemAndChildren = itemService.GetItemAndChildren(items, rootItem);
        (int width, int height) = itemService.CalculateItemSize(itemAndChildren);
        var x = rootItem.Location.Value1.x;
        var y = rootItem.Location.Value1.y;

        for (var dy = 0; dy < height; dy++)
        {
            for (var dx = 0; dx < width; dx++)
            {
                var tempX = x + dx;
                var tempY = y + dx;
                _matrix[tempY * primaryGrid.CellsHorizontal + tempX] = true;
            }
        }

        items.ForEach(i => ItemsMap[i.Id] = i);
    }

    /// <returns>The root item, not the full stack</returns>
    public ItemInstance FindItem(MongoId id)
    {
        if (!ItemsMap.TryGetValue(id, out var item))
        {
            item = null;
        }

        return item;
    }

    public void MoveItem(ItemService itemService, ItemFactoryService itemFactoryService, List<ItemInstance> items, LocationInGrid targetLocation)
    {
        if (items.Count == 0)
        {
            throw new Exception($"{nameof(items)}.Count must be greater than 0");
        }

        var rootItem = items[0];
        var stashItem = FindItem(Stash.Value);
        var template = itemFactoryService.ItemTemplates[stashItem.TemplateId];
        var compoundItemItemProperties = template.Props.ToObject<CompoundItemItemProperties>();
        var primaryGrid = compoundItemItemProperties.Grids[0].Properties;

        if (!rootItem.Location.IsValue1)
        {
            throw new Exception("!rootItem.Location.IsValue1");
        }

        var location = rootItem.Location.Value1;

        if (location == null)
        {
            throw new Exception("Location is null");
        }

        var itemAndChildren = itemService.GetItemAndChildren(items, rootItem);
        (int width, int height) = itemService.CalculateItemSize(itemAndChildren);
        var previousX = location.x;
        var previousY = location.y;
        var targetX = targetLocation.x;
        var targetY = targetLocation.y;

        for (var dy = 0; dy < height; dy++)
        {
            for (var dx = 0; dx < width; dx++)
            {
                var tempPreviousX = previousX + dx;
                var tempPreviousY = previousY + dy;

                var tempTargetX = targetX + dx;
                var tempTargetY = targetY + dy;

                _matrix[tempPreviousY * primaryGrid.CellsHorizontal + tempPreviousX] = false;
                _matrix[tempTargetY * primaryGrid.CellsHorizontal + tempTargetX] = true;
            }
        }

        rootItem.Location = targetLocation;
    }

    public List<ItemInstance> RemoveItem(ItemInstance item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (!ItemsMap.ContainsKey(item.Id))
        {
            return [];
        }

        if (!item.Location.IsValue1)
        {
            throw new Exception("!item.Location.IsValue1");
        }

        var location = item.Location.Value1;

        if (location == null)
        {
            throw new Exception("Location is null");
        }

        var containerItem = ItemsMap[item.ParentId];

        if (containerItem == null)
        {
            throw new Exception($"Failed to find container {item.ParentId} for {item.Id}");
        }

        var itemService = ItemService.Instance;
        var itemAndChildren = itemService.GetItemAndChildren(Items, item);
        var template = ItemFactoryService.Instance.ItemTemplates[containerItem.TemplateId];
        var compoundItemItemProperties = template.Props.ToObject<CompoundItemItemProperties>();
        var owningGrid = compoundItemItemProperties.Grids.Find(g => g.Name == item.SlotId)?.Properties;

        if (owningGrid == null)
        {
            throw new Exception($"Failed to find owning grid on {containerItem.Id}.{item.SlotId}");
        }

        (int width, int height) = itemService.CalculateItemSize(itemAndChildren);
        var previousX = location.x;
        var previousY = location.y;

        for (var dy = 0; dy < height; dy++)
        {
            for (var dx = 0; dx < width; dx++)
            {
                var tempPreviousX = previousX + dx;
                var tempPreviousY = previousY + dy;

                _matrix[tempPreviousY * owningGrid.CellsHorizontal + tempPreviousX] = false;
            }
        }

        itemAndChildren.ForEach(i => ItemsMap.Remove(i.Id));

        return itemAndChildren;
    }

    /// <returns>The item and children that were removed</returns>
    public List<ItemInstance> RemoveItem(MongoId id)
    {
        return RemoveItem(ItemsMap[id]);
    }

    public List<ItemInstance> GetItemAndChildren(ItemService itemService, MongoId id)
    {
        return itemService.GetItemAndChildren(Items, id);
    }

    public List<ItemInstance> GetItemsByTemplate(MongoId templateId)
    {
        return Items.FindAll(i => i.TemplateId == templateId);
    }

    public ItemInstance StashItem
    {
        get
        {
            if (Stash.HasValue && ItemsMap.TryGetValue(Stash.Value, out var itemInstance))
            {
                return itemInstance;
            }

            return null;
        }
    }

    private bool[] _matrix;
}