using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public partial class LocaleController : HttpController
    {
        [GeneratedRegex("^/client/locale/(?<languageId>[a-z]+(-[a-z]+)?)$")]
        private static partial Regex PathExpression();

        public LocaleController() : base(PathExpression())
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var parameters = context.GetPathParameters(this);

            var languageId = parameters["languageId"];
            var locale = EftOrm.GetGlobalLocale(languageId);
            var response = new ResponseBody<Dictionary<string, string>>
            {
                data = locale
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}
