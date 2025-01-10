using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Weather;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class LocalGameWeatherResponse
{
    [DataMember(Name = "season")]
    public byte Season { get; set; }

    [DataMember(Name = "weather")]
    public List<WeatherInfo> Weathers { get; set; }
}