using System.Text.Json.Serialization;

namespace WeatherForecast.Api.Dtos;

class WeatherDataDto
{
    public string Timezone { get; set; } = string.Empty;
    [JsonPropertyName("days")]
    public List<WeatherDayDataDto> WeatherDays {get;set;} = [];
}