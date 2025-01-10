using System.Threading.Tasks;
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
        var response = _weatherService.CreateLocalWeather();
        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}