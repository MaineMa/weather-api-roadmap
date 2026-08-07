namespace WeatherForecast.Api.Dtos;

class GetWeatherWithDatesRequestDto
{
    public double Latitude {get;set;}
    public double Longitude {get;set;}
    public string StartDate {get;set;} = string.Empty;
    public string EndDate {get;set;} = string.Empty;
}