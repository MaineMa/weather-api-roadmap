using System.Text.Json;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";

});

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

builder.Services.AddScoped<WeatherService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
