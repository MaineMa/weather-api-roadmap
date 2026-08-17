using WeatherForecast.Api.Models;

class WeatherDaysData
{   
    public string Timezone {get;set;} = string.Empty;
    public List<WeatherDayData> WeatherDays {get;set;} = [];
}