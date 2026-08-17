namespace WeatherForecast.Api.Repositories;

using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecast.Api.Dtos;

class WeatherForecastRepository(HttpClient httpClient, IConfiguration configuration, IOptions<JsonSerializerOptions> jsonOptions) : IWeatherForecastRepository
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value;


    public async Task<CurrentWeatherDataDto> GetCurrentWeatherAsync(double latitude, double longitude)
    {
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}/{today}?unitGroup=metric&key={apiKey}&contentType=json&lang=es&include=days%2Ccurrent";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<CurrentWeatherData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
        return new CurrentWeatherDataDto
        {
            Datetime = data.CurrentConditions.Datetime,
            Temp = data.CurrentConditions.Temp,
            Humidity = data.CurrentConditions.Humidity,
            Conditions = data.CurrentConditions.Conditions,
            ChanceOfRain = data.CurrentConditions.ChanceOfRain
        };
    }

    public async Task<WeatherDataDto> GetLastWeekWeatherDataAsync(double latitude, double longitude)
    {
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var startDate = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
        var endDate = DateTime.Today.ToString("yyyy-MM-dd");
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}/{startDate}/{endDate}?unitGroup=metric&key={apiKey}&contentType=json&lang=es&include=days";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WeatherDaysData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
        return new WeatherDataDto
        {
            Timezone = data.Timezone,
            WeatherDays = [.. data.WeatherDays.Select(d => new WeatherDayDataDto
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
    }

    public async Task<TodayWeatherDataDto> GetTodaysWeatherDataAsync(double latitude, double longitude)
    {
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}/{today}?unitGroup=metric&key={apiKey}&contentType=json&lang=es&include=days";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WeatherDaysData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
        var day = data.WeatherDays.FirstOrDefault() ?? throw new Exception("No se encontraron datos del día actual.");
        return new TodayWeatherDataDto 
        {
            Datetime = day.Datetime,
            Timezone = data.Timezone,
            Conditions = day.Conditions,
            TempMax = day.TempMax,
            TempMin = day.TempMin,
            ChanceOfRain = day.ChanceOfRain,
            Humidity = day.Humidity,
            Description = day.Description
        };
    }

    public async Task<WeatherDataDto> GetWeatherDataAsync(double latitude, double longitude, string startDate, string endDate)
    {
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}/{startDate}/{endDate}?unitGroup=metric&key={apiKey}&contentType=json&lang=es&include=days";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WeatherDaysData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
        return new WeatherDataDto
        {
            Timezone = data.Timezone,
            WeatherDays = [.. data.WeatherDays.Select(d => new WeatherDayDataDto
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
    }

    public async Task<WeatherDataDto> GetWeatherForecastDataAsync(double latitude, double longitude)
    {
        var apiKey = _configuration["WeatherApi:ApiKey"];
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}?unitGroup=metric&include=days&key={apiKey}&contentType=json&lang=es";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WeatherDaysData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
        return new WeatherDataDto
        {
            Timezone = data.Timezone,
            WeatherDays = [.. data.WeatherDays.Select(d => new WeatherDayDataDto
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
    }
}