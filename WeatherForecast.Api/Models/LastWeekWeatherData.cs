using System.Text.Json.Serialization;
using WeatherForecast.Api.Models;

class LastWeekWeatherData
{
    [JsonPropertyName("days")]
    public List<WeatherDayData> WeatherDays {get;set;} = [];
}