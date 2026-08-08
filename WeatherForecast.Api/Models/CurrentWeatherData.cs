using WeatherForecast.Api.Models;

class CurrentWeatherData
{
    public WeatherHourlyData CurrentConditions {get;set;} = new();
}