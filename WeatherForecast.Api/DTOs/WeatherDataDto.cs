namespace WeatherForecast.Api.Dtos;

class WeatherDataDto
{
    public string Timezone { get; set; } = string.Empty;
    public List<WeatherDayDataDto> WeatherDays {get;set;} = [];
}