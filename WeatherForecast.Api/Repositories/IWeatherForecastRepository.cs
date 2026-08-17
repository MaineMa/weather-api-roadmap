namespace WeatherForecast.Api.Repositories;

using WeatherForecast.Api.Dtos;

interface IWeatherForecastRepository
{
    Task<TodayWeatherDataDto> GetTodaysWeatherDataAsync(double latitude, double longitude);
    Task<WeatherDataDto> GetWeatherDataAsync(GetWeatherWithDatesRequestDto request);
    Task<CurrentWeatherDataDto> GetCurrentWeatherAsync(double latitude, double longitude);
    Task<WeatherDataDto> GetLastWeekWeatherDataAsync(double latitude, double longitude);
    Task<WeatherDataDto> GetWeatherForecastDataAsync(double latitude, double longitude);
}