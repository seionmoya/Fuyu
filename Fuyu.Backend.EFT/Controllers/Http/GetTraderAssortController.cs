using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public partial class GetTraderAssortController : AbstractEftHttpController
{
    [GeneratedRegex("/client/trading/api/getTraderAssort/(?<traderId>[A-Za-z0-9]+)")]
    private static partial Regex PathExpression();

    public GetTraderAssortController() : base(PathExpression())
    {
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var parameters = context.GetPathParameters(this);
        var traderId = parameters["traderId"];

        // TODO: handle this
        // --seionmoya, 2024-11-18
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

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}