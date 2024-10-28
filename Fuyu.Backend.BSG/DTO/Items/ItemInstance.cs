using System;
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

        public T GetUpd<T>()
        {
            // NOTE: ??= means assign thisUpd to upd but if upd is null create a new instance
            // -- nexus4880, 2024-10-27
            var thisUpd = upd ??= new ItemUpdatable();

			// NOTE: Intentionally letting this throw here. The idea is that GetUpd should
			// create T if it doesn't exist meaning most usage would be GetUpd<Upd>().Value
            // which means a null check after calling this would be undesirable
            // -- nexus4880, 2024-10-27
			var field = thisUpd.GetType().GetFields().First(f => f.FieldType == typeof(T));
            var updValue = field.GetValue(thisUpd);

            if (updValue == null)
            {
                updValue = Activator.CreateInstance(typeof(T));
                field.SetValue(thisUpd, updValue);
            }

            return (T)updValue;
        }
    }
}