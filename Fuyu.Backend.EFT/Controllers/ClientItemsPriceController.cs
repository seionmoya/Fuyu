using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Backend.EFT.DTO.Trading;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers
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
			var account = EftOrm.GetAccount(context.GetSessionId());
			var profile = EftOrm.GetProfile(account.PveId);
			var response = new ResponseBody<Union<SupplyData, Dictionary<MongoId, float>>>();

			if (parameters.TryGetValue("traderId", out var traderId))
			{
				// trader
				var currencyCourses = new Dictionary<MongoId, double> {
						{ "5449016a4bdc2d6f028b456f", 1d },
						{ "5696686a4bdc2da3298b456a", 1d },
						{ "569668774bdc2da2298b4568", 1d },
						{ "5d235b4d86f7742e017bc88a", 1d }
				};

				response.data = new SupplyData
				{
					CurrencyCourses = currencyCourses,
					MarketPrices = profile.Pmc.Inventory.Items.DistinctBy(i => i._tpl).ToDictionary(i => i._tpl, _ => 1000d),
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
