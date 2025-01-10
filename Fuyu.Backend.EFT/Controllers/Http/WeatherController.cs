using System;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;

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
        var weather = _weatherService.CreateMainMenuWeather();
        var dateTime = DateTime.Now;

        var response = new WeatherResponse()
        {
            Weather = weather,
            Acceleration = 0f,
            Time = dateTime.ToString("HH:mm:ss"),
            Date = dateTime.ToString("d")

        };

        var text = response.ToString();
        return context.SendJsonAsync(text, true, true);
    }
}