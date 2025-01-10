using System;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Weather;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class WeatherController : AbstractEftHttpController
{
    private readonly WeatherService _weatherService;

    public WeatherController() : base("/client/weather")
    {
        _weatherService = WeatherService.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var localWeather = _weatherService.CreateLocalWeather();
        var response = new ResponseBody<WeatherResponse>()
        {
            data = localWeather
        };
        
        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}