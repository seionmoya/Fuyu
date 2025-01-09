using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemInstance
{
    private readonly ItemFactoryService _itemFactoryService;

    public ItemInstance()
    {
        _itemFactoryService = ItemFactoryService.Instance;
    }

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

    public T GetOrCreateUpdatable<T>() where T : class
    {
        if (Updatable == null)
        {
            Updatable = _itemFactoryService.CreateItemUpdatable(TemplateId);
        }

        // NOTE: Intentionally letting this throw here. The idea is that GetOrCreateUpdatable should
        // create T if it doesn't exist meaning most usage would be GetOrCreateUpdatable<Upd>().Value
        // which means a null check after calling this would be undesirable
        // -- nexus4880, 2024-10-27
        var field = Updatable.GetType()
            .GetProperties()
            .First(f => f.PropertyType == typeof(T));

        var value = field.GetValue(Updatable) as T;

        return value;
    }
}