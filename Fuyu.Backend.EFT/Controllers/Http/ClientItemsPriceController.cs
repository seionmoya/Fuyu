using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Trading;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public partial class ClientItemsPriceController : HttpController
	{
		[GeneratedRegex(@"^/client/items/prices(/(?<traderId>[A-Za-z0-9]+))?$")]
		private static partial Regex PathExpression();

		public ClientItemsPriceController() : base(PathExpression())
		{
		}

		public override Task RunAsync(HttpContext context)
		{
			var parameters = context.GetPathParameters(this);
			var profile = EftOrm.GetActiveProfile(context.GetSessionId());
			var response = new ResponseBody<Union<SupplyData, Dictionary<MongoId, float>>>();

			if (parameters.TryGetValue("traderId", out var traderId))
			{
				// trader

				// NOTE: taken from dump client.items.prices
				// -- nexus4880, 2024-10-31
				var currencyCourses = new Dictionary<MongoId, double>
				{
					{ "5449016a4bdc2d6f028b456f", 1d    },	// RUB
					{ "569668774bdc2da2298b4568", 144d  },	// EUR
					{ "5696686a4bdc2da3298b456a", 136d  },	// USD
					{ "5d235b4d86f7742e017bc88a", 7500d }	// GP Coin
				};

				response.data = new SupplyData
				{
					CurrencyCourses = currencyCourses,
					// Every item is worth 1000 RUB for testing
					MarketPrices = profile.Pmc.Inventory.Items.DistinctBy(i => i.TemplateId).ToDictionary(i => i.TemplateId, _ => 1000d),
					SupplyNextTime = (int)TimeSpan.FromSeconds(5d).Ticks
				};
			}
			else
			{
				// ragfair
				response.data = new Dictionary<MongoId, float>();
			}
			
			return context.SendJsonAsync(Json.Stringify(response));
		}
	}
}
