using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public partial class MenuLocaleController : AbstractEftHttpController
    {
        [GeneratedRegex("^/client/menu/locale/(?<languageId>[a-z]+(-[a-z]+)?)$")]
        private static partial Regex PathExpression();

        public MenuLocaleController() : base(PathExpression())
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var parameters = context.GetPathParameters(this);

            var languageId = parameters["languageId"];
            var locale = EftOrm.Instance.GetMenuLocale(languageId);
            var response = new ResponseBody<MenuLocaleResponse>
            {
                data = locale
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}
