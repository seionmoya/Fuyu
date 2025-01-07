using System;
using System.Collections.Generic;
using System.Reflection;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.BSG.DTO.Services
{
    // TODO: split UPD factory and Item Factory
    public class ItemFactoryService
    {
        public static ItemFactoryService Instance => instance.Value;
        private static readonly Lazy<ItemFactoryService> instance = new(() => new ItemFactoryService());

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private ItemFactoryService()
        {

        }

        public Dictionary<MongoId, ItemTemplate> ItemTemplates { get; private set; }

        public void Load()
        {
            var itemsText = Resx.GetText("eft", "database.client.items.json");
            ItemTemplates = Json.Parse<ResponseBody<Dictionary<MongoId, ItemTemplate>>>(itemsText).data;
        }

        public ItemInstance CreateItem(MongoId tpl, MongoId? id = null)
        {
            var template = ItemTemplates[tpl];
            var itemId = id.GetValueOrDefault(new MongoId(true));
            ItemUpdatable upd = CreateItemUpdatable(template);

            var item = new ItemInstance
            {
                Id = itemId,
                TemplateId = tpl,
                Updatable = upd
            };

            return item;
        }

        public ItemUpdatable CreateItemUpdatable(ItemTemplate template)
        {
            ItemUpdatable upd = null;
            var updProperties = typeof(ItemUpdatable).GetProperties();

            foreach (var updProperty in updProperties)
            {
                var component = CreateItemComponent(template, updProperty.PropertyType, false);
                if (component != null)
                {
                    if (upd == null)
                    {
                        upd = new ItemUpdatable();
                    }

                    updProperty.SetValue(upd, component);
                }
            }

            return upd;
        }

        public object CreateItemComponent(ItemTemplate template, Type componentType, bool createDefault)
        {
            var isItemComponentInterface = typeof(IItemComponent).IsAssignableFrom(componentType);
            if (!isItemComponentInterface)
            {
                return null;
            }

            var bindingFlags = BindingFlags.Public | BindingFlags.Static;
            var createComponentMethodName = nameof(IItemComponent.CreateComponent);
            var createComponentMethod = componentType.GetMethod(createComponentMethodName, bindingFlags);
            if (createComponentMethod == null)
            {
                return null;
            }

            var result = createComponentMethod.Invoke(null, [template.Props]);
            if (result == null && createDefault)
            {
                result = Activator.CreateInstance(componentType);
            }

            return result;
        }

        public ItemUpdatable CreateItemUpdatable(MongoId tpl)
        {
            return CreateItemUpdatable(ItemTemplates[tpl]);
        }

        public object CreateItemComponent(MongoId tpl, Type componentType, bool createDefault)
        {
            return CreateItemComponent(ItemTemplates[tpl], componentType, createDefault);
        }
    }
}
