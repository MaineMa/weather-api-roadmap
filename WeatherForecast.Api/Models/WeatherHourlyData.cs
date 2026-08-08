using System.Text.Json.Serialization;

namespace WeatherForecast.Api.Models;

class WeatherHourlyData
{
    public string Datetime {get; set;} = string.Empty;
    public double Temp {get; set;}
    public double Humidity {get; set;}
    public string Conditions {get; set;} = string.Empty;
    [JsonPropertyName("precipprob")]
    public double ChanceOfRain {get;set;}
}