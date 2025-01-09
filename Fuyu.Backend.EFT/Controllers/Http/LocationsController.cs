using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocationsController : AbstractEftHttpController
    {
        private readonly LocationService _locationService;

        public LocationsController() : base("/client/locations")
        {
            _locationService = LocationService.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var worldmap = _locationService.GetWorldMap();
            var response = new ResponseBody<WorldMap>()
            {
                data = worldmap
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}