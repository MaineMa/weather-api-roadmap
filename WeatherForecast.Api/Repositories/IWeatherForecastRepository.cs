namespace WeatherForecast.Api.Repositories;

using WeatherForecast.Api.Dtos;

interface IWeatherForecastRepository
{
    Task<TodayWeatherDataDto> GetTodaysWeatherAsync();
}