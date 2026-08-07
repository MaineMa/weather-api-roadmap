namespace WeatherForecast.Api.Dtos;

class CurrentWeatherDataDto
{
    public string Datetime {get;set;} = string.Empty;
    public double Temp {get;set;}
    public double Humidity {get; set;}
    public double ChanceOfRain { get; set; }
    public string Conditions {get; set;} = string.Empty;
}