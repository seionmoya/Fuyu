using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Weather;

[DataContract]
public class LocalWeather
{
    [DataMember(Name = "season")]
    public byte Season { get; set; }

    [DataMember(Name = "weather")]
    public List<Weather> Weathers { get; set; }
}