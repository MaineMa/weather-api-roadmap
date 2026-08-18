namespace WeatherForecast.Api.Services;

using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Repositories;

class WeatherService(IDistributedCache _cache, IWeatherForecastRepository _repository)
{
    private readonly IDistributedCache _cache = _cache;
    private readonly IWeatherForecastRepository _repository = _repository;

    public async Task<WeatherDayData> GetTodayWeatherAsync(double latitude, double longitude)
    {
        string key = $"weather:today:${latitude}-${longitude}";
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData is not null)
            return JsonSerializer.Deserialize<WeatherDayData>(cachedData)!;

        var dto = await _repository.GetTodaysWeatherDataAsync(latitude,longitude);
        WeatherDayData data = new()
            {
                Datetime = dto.Datetime,
                Conditions = dto.Conditions,
                TempMax = dto.TempMax,
                TempMin = dto.TempMin,
                ChanceOfRain = dto.ChanceOfRain,
                Humidity = dto.Humidity,
                Description = dto.Description
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

    public async Task<WeatherDaysData> GetWeatherDataAsync(double latitude, double longitude, string startDate, string endDate)
    {
        string key = $"weather:${startDate}:${endDate}:${latitude}-${longitude}";
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData is not null)
            return JsonSerializer.Deserialize<WeatherDaysData>(cachedData)!;

        var dto = await _repository.GetWeatherDataAsync(latitude,longitude,startDate,endDate);
        WeatherDaysData data = new()
        {
            Timezone = dto.Timezone,
            WeatherDays = [.. dto.WeatherDays.Select(d => new WeatherDayData
                {
                    Datetime = d.Datetime,
                    Conditions = d.Conditions,
                    TempMax = d.TempMax,
                    TempMin = d.TempMin,
                    ChanceOfRain = d.ChanceOfRain,
                    Humidity = d.Humidity,
                    Description = d.Description
                })]
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

    public async Task<WeatherDaysData> GetLastWeekWeatherAsync(double latitude, double longitude)
    {
        string key = $"weather:lastweek:${latitude}-${longitude}";
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData is not null)
            return JsonSerializer.Deserialize<WeatherDaysData>(cachedData)!;
        
        var dto = await _repository.GetLastWeekWeatherDataAsync(latitude,longitude);
        WeatherDaysData data = new()
        {
            Timezone = dto.Timezone,
            WeatherDays = [.. dto.WeatherDays.Select(d => new WeatherDayData
                {
                    Datetime = d.Datetime,
                    Conditions = d.Conditions,
                    TempMax = d.TempMax,
                    TempMin = d.TempMin,
                    ChanceOfRain = d.ChanceOfRain,
                    Humidity = d.Humidity,
                    Description = d.Description
                })]
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

    public async Task<WeatherDaysData> GetWeatherForecastAsync(double latitude, double longitude)
    {
        string key = $"weather:forecast:${latitude}-${longitude}";
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData is not null)
            return JsonSerializer.Deserialize<WeatherDaysData>(cachedData)!;
        
        var dto = await _repository.GetWeatherForecastDataAsync(latitude,longitude);
        WeatherDaysData data = new()
        {
            Timezone = dto.Timezone,
            WeatherDays = [.. dto.WeatherDays.Select(d => new WeatherDayData
                {
                    Datetime = d.Datetime,
                    Conditions = d.Conditions,
                    TempMax = d.TempMax,
                    TempMin = d.TempMin,
                    ChanceOfRain = d.ChanceOfRain,
                    Humidity = d.Humidity,
                    Description = d.Description
                })]
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