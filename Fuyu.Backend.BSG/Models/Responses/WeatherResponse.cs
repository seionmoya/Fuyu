using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Weather;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class WeatherResponse
{
    [DataMember(Name = "weather", EmitDefaultValue = false)]
    public WeatherInfo Weather { get; set; }

    [DataMember(Name = "acceleration")]
    public float Acceleration { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    public string Date { get; set; }

    [DataMember(Name = "time", EmitDefaultValue = false)]
    public string Time { get; set; }

    
}
