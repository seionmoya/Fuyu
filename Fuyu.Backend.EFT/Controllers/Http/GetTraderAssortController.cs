using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Trading;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers
{
	public partial class GetTraderAssortController : HttpController
	{
		[GeneratedRegex("/client/trading/api/getTraderAssort/(?<traderId>[A-Za-z0-9]+)")]
		private static partial Regex PathExpression();

		public GetTraderAssortController() : base(PathExpression())
		{
		}

		public override Task RunAsync(HttpContext context)
		{
			var parameters = context.GetPathParameters(this);
			var traderId = parameters["traderId"];
			var response = new ResponseBody<TraderAssort>
			{
				data = new TraderAssort
				{
					ExchangeRate = 0d,
					Items = [],
					BarterScheme = [],
					LoyaltyLevelItems = [],
					NextResupply = int.MaxValue
				}
			};

			return context.SendJsonAsync(Json.Stringify(response));
		}
	}
}
