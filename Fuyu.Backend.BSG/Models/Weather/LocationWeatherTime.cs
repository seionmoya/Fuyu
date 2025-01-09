using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Weather;

[DataContract]
public class LocationWeatherTime
{
    [DataMember(Name = "season")]
    public byte Season { get; set; }

    [DataMember(Name = "weather")]
    public Weather Weather { get; set; }

    [DataMember(Name = "acceleration")]
    public float Acceleration { get; set; }

    [DataMember(Name = "date")]
    public string Date { get; set; }

    [DataMember(Name = "time")]
    public string Time { get; set; }
}