using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Hashing;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class ClientInsuranceItemsListCostController : AbstractEftHttpController<InsuranceCostRequest>
{
    private readonly EftOrm _eftOrm;

    public ClientInsuranceItemsListCostController() : base("/client/insurance/items/list/cost")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context, InsuranceCostRequest body)
    {
        var profile = _eftOrm.GetActiveProfile(context.GetSessionId());
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

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}