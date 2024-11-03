using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Services;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
	public class ItemInstance
    {
        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

		[DataMember(Name = "_tpl")]
        public MongoId TemplateId { get; set; }

		// emits when 'null'
		[DataMember(Name = "parentId", EmitDefaultValue = false)]
        public MongoId? ParentId { get; set; }

		// emits when 'null'
		[DataMember(Name = "slotId", EmitDefaultValue = false)]
        public string SlotId { get; set; }

		// emits when 'null'
		[DataMember(Name = "location", EmitDefaultValue = false)]
        public Union<LocationInGrid, int> Location { get; set; }

		// emits when 'null'
		[DataMember(Name = "upd", EmitDefaultValue = false)]
        public ItemUpdatable Updatable { get; set; }

        public T GetUpdatable<T>(bool createIfNull = true) where T: class, new()
        {
			var targetProperty = typeof(ItemUpdatable).GetProperties().First(p => p.PropertyType == typeof(T));
            object currentValue = null;

			if (Updatable != null)
			{
				currentValue = targetProperty.GetValue(Updatable);
			}

            if (currentValue == null && createIfNull)
			{
				currentValue = ItemFactoryService.CreateItemComponent(TemplateId, typeof(T), true);
				if (Updatable == null)
				{
					Updatable = new ItemUpdatable();
				}

				targetProperty.SetValue(Updatable, currentValue);
			}

			return (T)currentValue;
		}
    }
}