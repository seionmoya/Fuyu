using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading;

[DataContract]
public class HandbookTemplates
{
    [DataMember(Name = "Categories")]
    public List<HandbookCategory> Categories { get; set; }

    [DataMember(Name = "Items")]
    public List<HandbookItem> Items { get; set; }

    public List<HandbookCategory> GetAllCategoriesOfType(HandbookCategory root)
    {
        var result = new List<HandbookCategory> { root };
        var added = true;

        while (added)
        {
            added = false;

            for (var i = 0; i < Categories.Count; i++)
            {
                var category = Categories[i];

                if (category.ParentId.HasValue &&
                    result.Exists(c => c.Id == category.ParentId.Value) &&
                    !result.Exists(c => c.Id == category.Id))
                {
                    result.Add(category);
                    added = true;
                }
            }
        }

        return result;
    }

    public List<HandbookItem> GetAllItemsOfType(MongoId id)
    {
        var items = new List<HandbookItem>();

        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];

            if (item.ParentId == id)
            {
                items.Add(item);
            }
        }

        return items;
    }

    public List<HandbookItem> GetAllItemsOfTypeAndSubcategories(MongoId id)
    {
        var items = new List<HandbookItem>();

        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];

            if (item.ParentId == id)
            {
                items.Add(item);
            }
        }

        return items;
    }
}