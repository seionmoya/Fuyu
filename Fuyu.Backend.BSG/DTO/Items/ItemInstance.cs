using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
	public class ItemInstance
    {
        [DataMember]
        public MongoId _id;

        [DataMember]
        public MongoId _tpl;

        // emits when 'null'
        [DataMember(EmitDefaultValue = false)]
        public MongoId? parentId;

        // emits when 'null'
        [DataMember(EmitDefaultValue = false)]
        public string slotId;

        // emits when 'null'
        [DataMember(EmitDefaultValue = false)]
        public Union<LocationInGrid, int> location;

        // emits when 'null'
        [DataMember(EmitDefaultValue = false)]
        public ItemUpdatable upd;

        public T GetUpdatable<T>() where T: class, new()
        {
            if (upd == null)
            {
                upd = new ItemUpdatable();
            }

			// NOTE: Intentionally letting this throw here. The idea is that GetUpd should
			// create T if it doesn't exist meaning most usage would be GetUpd<Upd>().Value
            // which means a null check after calling this would be undesirable
            // -- nexus4880, 2024-10-27
			var field = upd.GetType().GetFields().First(f => f.FieldType == typeof(T));
            var value = field.GetValue(upd) as T;

            if (value == null)
            {
				value = new T();
                field.SetValue(upd, value);
            }

            return value;
        }
    }
}