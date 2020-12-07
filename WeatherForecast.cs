using System;

namespace AuthService
{
    public class WeatherForecast
    {
        public int Index { get ; set; }
        
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
