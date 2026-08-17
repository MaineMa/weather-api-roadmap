using System.Text.Json.Serialization;

namespace WeatherForecast.Api.Dtos;

class WeatherDayDataDto
{
    public string Datetime { get; set; } = string.Empty;
    public string Conditions { get; set; } = string.Empty;
    public double TempMax { get; set; }
    public double TempMin { get; set; }
    [JsonPropertyName("precipprob")]
    public double ChanceOfRain { get; set; }
    public double Humidity {get; set;}
    public string Description {get; set;} = string.Empty;
}