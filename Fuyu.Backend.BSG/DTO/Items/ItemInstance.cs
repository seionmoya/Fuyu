using System.Linq;
using System.Runtime.Serialization;
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

        public T GetUpdatable<T>() where T: class, new()
        {
            if (Updatable == null)
            {
                Updatable = new ItemUpdatable();
            }

			// NOTE: Intentionally letting this throw here. The idea is that GetUpdatable should
			// create T if it doesn't exist meaning most usage would be GetUpdatable<Upd>().Value
			// which means a null check after calling this would be undesirable
			// -- nexus4880, 2024-10-27
			var field = Updatable.GetType()
                .GetProperties()
                .First(f => f.PropertyType == typeof(T));

            var value = field.GetValue(Updatable) as T;

            if (value == null)
            {
				value = new T();
                field.SetValue(Updatable, value);
            }

            return value;
        }
    }
}