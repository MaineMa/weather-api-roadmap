namespace WeatherForecast.Api.Services;

using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherForecast.Api.Repositories;

class WeatherService(IDistributedCache _cache, IWeatherForecastRepository _repository)
{
    private readonly IDistributedCache _cache = _cache;
    private readonly IWeatherForecastRepository _repository = _repository;

    public async Task<CurrentWeatherData> GetCurrentWeatherAsync(double latitude, double longitude)
    {
        string key = $"weather:current:${latitude}-${longitude}";
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData is not null)
            return JsonSerializer.Deserialize<CurrentWeatherData>(cachedData)!;

        var dto = await _repository.GetCurrentWeatherAsync(latitude,longitude);
        CurrentWeatherData data = new()
        {
            CurrentConditions = new()
            {
                Datetime = dto.Datetime,
                Temp = dto.Temp,
                Humidity = dto.Humidity,
                Conditions = dto.Conditions,
                ChanceOfRain = dto.ChanceOfRain
            }
        };

        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(data),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            }
        );

        return data;
    }


}