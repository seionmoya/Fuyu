using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public partial class MenuLocaleController : HttpController
    {
        [GeneratedRegex("^/client/menu/locale/(?<languageId>[a-z]+(-[a-z]+)?)$")]
        private static partial Regex PathExpression();

        public MenuLocaleController() : base(PathExpression())
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var parameters = context.GetPathParameters(this);

            var languageId = parameters["languageId"];
            var locale = EftOrm.GetMenuLocale(languageId);
            var response = new ResponseBody<MenuLocaleResponse>
            {
                data = locale
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}
