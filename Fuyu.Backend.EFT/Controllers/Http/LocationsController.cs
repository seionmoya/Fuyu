using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocationsController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public LocationsController() : base("/client/locations")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var json = _eftOrm.GetLocations();
            var response = Json.Parse<ResponseBody<WorldMap>>(json);
            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}