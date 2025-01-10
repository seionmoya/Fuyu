using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Weather;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class WeatherResponse
{
    [DataMember(Name = "season")]
    public ESeason Season { get; set; }
    
    [DataMember(Name = "weather")]
    public Weather.WeatherNode WeatherNode { get; set; }
    
    [DataMember(Name = "acceleration")]
    public float Acceleration;

    [DataMember(Name = "date")]
    public string Date;

    [DataMember(Name = "time")]
    public string Time;
}