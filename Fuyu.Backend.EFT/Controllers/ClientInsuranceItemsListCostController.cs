using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Requests;
using Fuyu.Backend.EFT.DTO.Responses;
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
			var account = EftOrm.GetAccount(context.GetSessionId());
			var profile = EftOrm.GetProfile(account.PveId);
			var items = profile.Pmc.Inventory.items.FindAll(i => body.ItemIds.Contains(i._id));
			if (items.Count != body.ItemIds.Length)
			{
				return context.SendJsonAsync(Json.Stringify(new ResponseBody<InsuranceCostResponse>
				{
					errmsg = "One or more items could not be found on the backend"
				}));
			}

			var response = new InsuranceCostResponse(body.Traders.Length);
			var prices = items.ToDictionary(i => i._tpl, _ => 1);

			foreach (var trader in body.Traders)
			{
				response[trader] = prices;
			}

			return context.SendJsonAsync(Json.Stringify(new ResponseBody<InsuranceCostResponse>
			{
				data = response
			}));
		}
	}
}
