using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Weather;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class LocalGameWeatherController : AbstractEftHttpController
{
    private readonly WeatherService _weatherService;

    public LocalGameWeatherController() : base("/client/localGame/weather")
    {
        _weatherService = WeatherService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var locationWeatherTime = _weatherService.CreateWeatherTime();
        var response = new ResponseBody<LocationWeatherTime>()
        {
            data = locationWeatherTime
        };

        return context.SendJsonAsync(Json.Stringify(response), true, true);
    }
}