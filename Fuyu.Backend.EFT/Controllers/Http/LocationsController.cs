using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocationsController : HttpController
    {
        public LocationsController() : base("/client/locations")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var json = EftOrm.GetLocations();
            var locations = Json.Parse<ResponseBody<WorldMap>>(json);
            var response = Json.Stringify(locations);
            return context.SendJsonAsync(response);
        }
    }
}