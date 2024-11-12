using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Requests;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers
{
	public class ClientInsuranceItemsListCostController : HttpController<InsuranceCostRequest>
	{
		public ClientInsuranceItemsListCostController() : base("/client/insurance/items/list/cost")
		{
		}

		public override Task RunAsync(HttpContext context, InsuranceCostRequest body)
		{
			var profile = EftOrm.GetActiveProfile(context.GetSessionId());
			var items = profile.Pmc.Inventory.Items.FindAll(i => body.ItemIds.Contains(i.Id));
			var response = new ResponseBody<InsuranceCostResponse>();

			if (items.Count == body.ItemIds.Length)
			{
				var insuranceCost = new InsuranceCostResponse(body.Traders.Length);
				var prices = new Dictionary<MongoId, int>(items.Count);

				foreach (var item in items)
				{
					prices[item.TemplateId] = 1;
				}

				foreach (var trader in body.Traders)
				{
					insuranceCost[trader] = prices;
				}

				response.data = insuranceCost;
			}
			else
			{
				response.errmsg = "One or more items could not be found on the backend";
			}

			return context.SendJsonAsync(Json.Stringify(response));
		}
	}
}
