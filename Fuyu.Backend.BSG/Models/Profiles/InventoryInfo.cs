using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Hashing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fuyu.Backend.BSG.Models.Profiles;

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

        EnsureMatrixGenerated(itemService, itemFactoryService);

        if (!Stash.HasValue)
        {
            return;
        }

        var stashItem = Items.Find(i => i.Id == Stash.Value);
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

        Items.AddRange(items);
    }

    public ItemInstance FindItem(MongoId id)
    {
        return Items.Find(i => i.Id == id);
    }

    public void MoveItem(ItemService itemService, ItemFactoryService itemFactoryService, List<ItemInstance> items, LocationInGrid targetLocation)
    {
        if (items.Count == 0)
        {
            throw new Exception($"{nameof(items)}.Count must be greater than 0");
        }

        var rootItem = items[0];
        var stashItem = Items.Find(i => i.Id == Stash.Value);
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
                var tempPreviousY = previousY + dx;

                var tempTargetX = targetX + dx;
                var tempTargetY = targetY + dx;

                _matrix[tempPreviousY * primaryGrid.CellsHorizontal + tempPreviousX] = false;
                _matrix[tempTargetY * primaryGrid.CellsHorizontal + tempTargetX] = true;
            }
        }

        rootItem.Location = targetLocation;
    }

    public ItemInstance RemoveItem(ItemInstance item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (Items.Remove(item))
        {
            EnsureMatrixGenerated(null, null, true);
            return item;
        }

        return null;
    }

    public ItemInstance RemoveItem(MongoId id)
    {
        return RemoveItem(Items.Find(i => i.Id == id));
    }

    public void RemoveItem(ItemService itemService, ItemFactoryService itemFactoryService, List<ItemInstance> items)
    {
        foreach (var item in items)
        {
            var owningItem = Items.Find(i => i.Id == item.ParentId);
            var owningItemProperties = itemFactoryService.GetItemProperties<CompoundItemItemProperties>(owningItem.TemplateId);
            var owningGridProperties = owningItemProperties.Grids?.Find(i => i.Name == item.SlotId)?.Properties;

            if (owningGridProperties == null)
            {
                throw new Exception($"Failed to find {nameof(owningGridProperties)}");
            }

            var itemAndChildren = itemService.GetItemAndChildren(Items, item);
            (int width, int height) = itemService.CalculateItemSize(itemAndChildren);
            var x = item.Location.Value1.x;
            var y = item.Location.Value1.y;

            for (var dy = 0; dy < height; dy++)
            {
                for (var dx = 0; dx < width; dx++)
                {
                    var tempX = x + dx;
                    var tempY = y + dx;

                    _matrix[tempY * owningGridProperties.CellsHorizontal + tempX] = false;
                }
            }
        }

        Items.RemoveAll(items.Contains);
    }

    public List<ItemInstance> GetItemAndChildren(ItemService itemService, MongoId id)
    {
        return itemService.GetItemAndChildren(Items, id);
    }

    public List<ItemInstance> GetItemsByTemplate(MongoId templateId)
    {
        return Items.FindAll(i => i.TemplateId == templateId);
    }

    public IEnumerable<ItemInstance> RootItems
    {
        get
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                if (item.SlotId == "hideout" && item.Location.IsValue1 && item.Location.Value1 != null)
                {
                    yield return item;
                }
            }
        }
    }

    public ItemInstance StashItem
    {
        get
        {
            if (Stash.HasValue)
            {
                return Items.Find(i => i.Id == Stash.Value);
            }

            return null;
        }
    }

    private bool[] _matrix;
}