using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LanguagesController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public LanguagesController() : base("/client/languages")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var languages = _eftOrm.GetLanguages();
            var response = new ResponseBody<Dictionary<string, string>>
            {
                data = languages
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}