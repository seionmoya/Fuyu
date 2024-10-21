using System.Collections.Generic;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Newtonsoft.Json;

namespace Fuyu.Backend.BSG.DTO.Services
{
	public static class ItemFactoryService
	{
		public static Dictionary<MongoId, ItemTemplate> ItemTemplates { get; private set; }

		public static void Load()
		{
			var itemsText = Resx.GetText("eft", "database.client.items.json");
			ItemTemplates = JsonConvert.DeserializeObject<Dictionary<MongoId, ItemTemplate>>(itemsText, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
			});
		}
	}
}
