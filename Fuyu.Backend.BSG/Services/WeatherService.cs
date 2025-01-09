using System;
using Fuyu.Backend.BSG.Models.Weather;

namespace Fuyu.Backend.BSG.Services;

public class WeatherService
{
    public static WeatherService Instance => instance.Value;
    private static readonly Lazy<WeatherService> instance = new(() => new WeatherService());

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private WeatherService()
    {

    }

    public LocationWeatherTime CreateWeatherTime()
    {
        var currentTime = DateTime.Now;
        return new LocationWeatherTime()
        {
            Date = currentTime.ToString("yyyy-MM-dd"),
            Time = currentTime.ToString("HH:mm:ss"),
            Acceleration = 0,
            Season = (byte)Random.Shared.Next(0, 5),
            Weather = CreateDefault()
        };
    }

    public Weather CreateDefault()
    {
        return new()
        {
            time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            cloud = -0.3f,
            wind_gustiness = 0,
            wind_direction = 7,
            rain = 1,
            rain_intensity = 0,
            fog = 0.004f,
            pressure = 760,
            temp = 20,
            wind_speed = 4
        };
    }

    // TODO: Make sure we generate default one then return edited values.
    public Weather CreateWeatherType(EWeatherType weatherType)
    {
        return CreateDefault();
    }

    public LocalWeather CreateLocalWeather()
    {
        var localWeather = new LocalWeather()
        {
            Season = (byte)Random.Shared.Next(0, 5),
            Weathers = []
        };

        for (int i = 0; i < (int)EWeatherType.None; i++)
        {
            var weather_for_type_i = CreateWeatherType((EWeatherType)i);
            localWeather.Weathers.Add(weather_for_type_i);
        }

        return localWeather;
    }
}