namespace WeatherForecast.Api.Repositories;

using WeatherForecast.Api.Dtos;

interface IWeatherForecastRepository
{
    Task<TodayWeatherDataDto> GetTodaysWeatherDataAsync(double Latitude, double Longitude);
    Task<WeatherDataDto> GetWeatherDataAsync(GetWeatherWithDatesRequestDto request);
    Task<CurrentWeatherDataDto> GetCurrentWeatherAsync(double Latitude, double Longitude);
    Task<WeatherDataDto> GetLastWeekWeatherDataAsync(double Latitude, double Longitude);
    Task<WeatherDataDto> GetWeatherForecastDataAsync(double Latitude, double Longitude);
}