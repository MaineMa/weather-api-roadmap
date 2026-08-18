namespace WeatherForecast.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Dtos;
using WeatherForecast.Api.Services;

[ApiController]
[Route("api/v1/weather")]
class WeatherForecastController : ControllerBase
{

    private readonly WeatherService _weatherService;

    public WeatherForecastController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetToday(double latitude, double longitude)
    {
        var result = await _weatherService.GetTodayWeatherAsync(latitude,longitude);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherByDate([FromQuery] GetWeatherWithDatesRequestDto request)
    {
        var result = await _weatherService.GetWeatherDataAsync(request.Latitude,
        request.Longitude,request.StartDate,request.EndDate);
        return Ok(result);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(double latitude, double longitude)
    {
        var result = await _weatherService.GetCurrentWeatherAsync(latitude,longitude);
        return Ok(result);
    }

    [HttpGet("last-week")]
    public async Task<IActionResult> GetLastWeek(double latitude, double longitude)
    {
        var result = await _weatherService.GetLastWeekWeatherAsync(latitude, longitude);
        return Ok(result);
    }

    [HttpGet("forecast")]
    public async Task<IActionResult> GetForecast(double latitude, double longitude)
    {
        var result = await _weatherService.GetWeatherForecastAsync(latitude,longitude);
        return Ok(result);
    }
}