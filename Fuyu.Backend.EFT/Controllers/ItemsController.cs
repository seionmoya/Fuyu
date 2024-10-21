using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Services;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.EFT.Controllers
{
    public class ItemsController : HttpController
    {
        private JsonSerializerSettings _settings;

        public ItemsController() : base("/client/items")
        {
            ItemFactoryService.Load();
            _settings = new JsonSerializerSettings
            {
                Converters =
                {
                    new StringEnumConverter()
                }
            };
		}

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(JsonConvert.SerializeObject(new ResponseBody<Dictionary<MongoId, ItemTemplate>>
            {
                data = ItemFactoryService.ItemTemplates
			}, _settings));
        }
    }
}