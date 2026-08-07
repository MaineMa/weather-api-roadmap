namespace WeatherForecast.Api.Dtos;

class TodayWeatherDataDto
{
    public double Latitude {get; set;}
    public double Longitude {get;set;}
    public string Datetime {get;set;} = string.Empty;
    public string Timezone { get; set; } = string.Empty;
    public string Conditions { get; set; } = string.Empty;
    public double TempMax {get;set;}
    public double TempMin {get;set;}
    public double Humidity {get; set;}
    public double ChanceOfRain { get; set; }
}