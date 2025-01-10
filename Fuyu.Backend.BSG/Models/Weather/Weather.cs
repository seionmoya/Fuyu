using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Weather;

[DataContract]
public class Weather
{
    [DataMember]
    public string time { get; set; }

    [DataMember]
    public float cloud { get; set; }

    [DataMember]
    public float wind_speed { get; set; }

    [DataMember]
    public int wind_direction { get; set; }

    [DataMember]
    public float wind_gustiness { get; set; }

    [DataMember]
    public float rain { get; set; }

    [DataMember]
    public float rain_intensity { get; set; }

    [DataMember]
    public float fog { get; set; }

    [DataMember]
    public float temp { get; set; }

    [DataMember]
    public float pressure { get; set; }
}