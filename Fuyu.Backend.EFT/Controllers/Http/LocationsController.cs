using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Locations;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocationsController : HttpController
    {
        public LocationsController() : base("/client/locations")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var json = EftOrm.GetLocations();
            var locations = Json.Parse<ResponseBody<WorldMap>>(json);
            var response = Json.Stringify(locations);
            await context.SendJsonAsync(response);
        }
    }
}