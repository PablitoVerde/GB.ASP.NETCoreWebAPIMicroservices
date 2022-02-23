using System;

namespace Lesson1
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public WeatherForecast()
        {

        }

        public WeatherForecast(DateTime dateTime, int temperatureC, string summary)
        {
            Date = dateTime;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        public WeatherForecast(string weatherForecast)
        {
            string[] str = weatherForecast.Split(' ');
            Date = DateTime.Parse(str[0]);
            TemperatureC = Convert.ToInt32(str[1]);
            Summary = str[2];
        }

        public override string ToString()
        {
            return $"{Date} {TemperatureC} {TemperatureF} {Summary}";
        }
    }
}
