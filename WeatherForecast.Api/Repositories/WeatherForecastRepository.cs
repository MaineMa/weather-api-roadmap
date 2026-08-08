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
        var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude}%2C{longitude}/{today}?unitGroup=metric&key={apiKey}&contentType=json&lang=es&include=days";
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
        var data = JsonSerializer.Deserialize<LastWeekWeatherData>(json,_jsonOptions) ?? throw new Exception("Couldn't deserialize weather response");
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

    public Task<TodayWeatherDataDto> GetTodaysWeatherDataAsync(double latitude, double longitude)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherDataDto> GetWeatherDataAsync(GetWeatherWithDatesRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherDataDto> GetWeatherForecastDataAsync(double latitude, double longitude)
    {
        throw new NotImplementedException();
    }
}