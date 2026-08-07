using System.ComponentModel;

class WeatherData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}