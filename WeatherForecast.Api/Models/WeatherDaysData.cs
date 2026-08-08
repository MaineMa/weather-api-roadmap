using System.Text.Json.Serialization;
using WeatherForecast.Api.Models;

class WeatherDaysData
{   
    public string Timezone {get;set;} = string.Empty;
    [JsonPropertyName("days")]
    public List<WeatherDayData> WeatherDays {get;set;} = [];
}