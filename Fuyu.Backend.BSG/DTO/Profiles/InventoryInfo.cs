using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using Fuyu.Backend.BSG.DTO.Services;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;

namespace Fuyu.Backend.BSG.DTO.Profiles
{
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

		public List<ItemInstance> RemoveItem(ItemInstance item)
		{
			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> RemoveItem(MongoId id)
		{
            var item = Items.Find(i => i.Id == id);
			if (item == null)
            {
                throw new Exception($"Failed to find item with id {id} in inventory");
            }

			return InventoryService.RemoveItem(this, item);
		}

        public List<ItemInstance> GetItemsByTemplate(MongoId tpl)
        {
            return Items.Where(i => i.TemplateId == tpl).ToList();
        }

        public ItemInstance GetStock(ItemInstance root)
		{
            var rootItemTemplate = ItemFactoryService.ItemTemplates[root.TemplateId];
            if (!rootItemTemplate.Props.ContainsKey("FoldedSlot"))
            {
                return null;
            }

            var weaponItemProperties = rootItemTemplate.Props.ToObject<WeaponItemProperties>();
			var subItems = ItemService.GetItemAndChildren(Items, root).Skip(1);

            return subItems.FirstOrDefault(i => i.SlotId == weaponItemProperties.FoldedSlot);
		}

        public Vector2 GetItemSize(ItemInstance root)
        {
            var rootItemTemplate = ItemFactoryService.ItemTemplates[root.TemplateId];
            var rootItemProperties = rootItemTemplate.Props.ToObject<ItemProperties>();
			var rootItemWidth = rootItemProperties.Width;
			var rootItemHeight = rootItemProperties.Height;
            var maxHeight = 0;
            var maxWidth = 0;
            var items = ItemService.GetItemAndChildren(Items, root);

			var foldable = root.GetUpdatable<ItemFoldableComponent>(false);
			if (foldable != null && foldable.Folded)
			{
                var stockItem = GetStock(root);

                if (stockItem == null)
                {
                    throw new Exception($"{root.Id} is folded but couldn't find stock?");
                }

                var stockItemProperties = ItemFactoryService
                    .ItemTemplates[stockItem.TemplateId]
                    .Props
                    .ToObject<StockItemProperties>();

                rootItemWidth -= stockItemProperties.SizeReduceRight;
			}

			// Skip the root item because we initialized rootItemWidth and rootItemHeight with it
			foreach (var item in items.Skip(1))
            {
                var itemTemplate = ItemFactoryService.ItemTemplates[item.TemplateId];
                var itemProperties = itemTemplate.Props.ToObject<ItemProperties>();
				var itemHeight = itemProperties.ExtraSizeUp + itemProperties.ExtraSizeDown;
				var itemWidth = itemProperties.ExtraSizeLeft + itemProperties.ExtraSizeRight;
				

				if (itemProperties.ExtraSizeForceAdd || foldable != null && !foldable.Folded)
				{
					rootItemWidth += itemWidth;
					rootItemHeight += itemHeight;
				}
                else
				{
					// Get the highest modifiers
					maxWidth = Math.Max(maxWidth, itemWidth);
					maxHeight = Math.Max(maxHeight, itemHeight);
				}
            }

            // Add the highest modifiers
			rootItemWidth += maxWidth;
			rootItemHeight += maxHeight;

            // if it's rotated then width becomes height (Y) and height becomes width (X)
            if (root.Location.IsValue1 && root.Location.Value1.r == EItemRotation.Vertical)
            {
                return new Vector2
                {
                    X = rootItemHeight,
                    Y = rootItemWidth
				};
            }

            return new Vector2
            {
                X = rootItemWidth,
                Y = rootItemHeight
            };
        }

		public LocationInGrid GetNextFreeSlot(int width, int height, out string gridName, EItemRotation desiredRotation = EItemRotation.Horizontal)
		{
            gridName = null;
            if (Stash == null)
            {
                return null;
            }

            var stashItem = Items.Find(i => i.Id == Stash.Value);
            if (stashItem == null)
            {
                return null;
            }

            var stashItemTemplate = ItemFactoryService.ItemTemplates[stashItem.TemplateId];
            var stashItemProps = stashItemTemplate.Props.ToObject<CompoundItemItemProperties>();

            foreach (var grid in stashItemProps.Grids)
			{
                gridName = grid.Name;
				var gridWidth = grid.Properties.CellsHorizontal;
				var gridHeight = grid.Properties.CellsVertical;
				var cells = new bool[gridWidth * gridHeight];
                var itemsInThisGrid = ItemService.GetItemAndChildren(Items, stashItem).Skip(1);

                foreach (var itemInThisGrid in itemsInThisGrid)
                {
                    if (!itemInThisGrid.Location.IsValue1)
                    {
                        continue;
                    }

                    var itemTemplate = ItemFactoryService.ItemTemplates[itemInThisGrid.TemplateId];
                    var itemProps = itemTemplate.Props.ToObject<ItemProperties>();
                    var itemLocation = itemInThisGrid.Location.Value1;
                    var itemSize = GetItemSize(itemInThisGrid);
                    var itemWidth = itemSize.X;
                    var itemHeight = itemSize.Y;

					for (var y = 0; y < itemHeight; y++)
					{
						for (var x = 0; x < itemWidth; x++)
						{
							var cellX = itemLocation.x + x;
							var cellY = itemLocation.y + y;
							cells[cellY * gridWidth + cellX] = true;
						}
					}
                }

				var builder = new StringBuilder(cells.Length);
				for (int i = 0; i < gridHeight; i++)
				{
					var line = cells.Skip(i * gridWidth).Take(gridWidth);
					builder.Append(line.Select(b => b ? 'X' : 'O').ToArray());
					if (i != gridHeight - 1)
					{
						builder.AppendLine();
					}
				}

				Console.Clear();
				Terminal.WriteLine(builder.ToString());

				for (var idx = 0; idx < cells.Length; idx++)
                {
                    if (cells[idx])
                    {
                        continue;
                    }

                    var x = idx % gridWidth;
                    var y = idx / gridWidth;
                    var found = true;

                    for (int dy = 0; found && dy < height; dy++)
					{
						for (int dx = 0; dx < width; dx++)
						{
                            var tempX = x + dx;
                            if (tempX > gridWidth)
                            {
                                found = false;
                                break;
                            }

                            var tempY = y + dy;
							if (tempY > gridHeight)
							{
								found = false;
								break;
							}

							var tempIdx = tempY * gridWidth + tempX;
                            if (cells[tempIdx])
                            {
                                found = false;
                                break;
                            }
						}
					}

                    if (found)
					{
                        return new LocationInGrid
                        {
                            x = x,
                            y = y,
                            r = desiredRotation
                        };
					}
                }
            }

            return null;
		}
	}
}