using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Lesson1
{
    /// <summary>
    /// Класс создания списка значений объектов класса WeatherForecast
    /// </summary>
    public class ValuesHolder
    {
        public ConcurrentDictionary<string, WeatherForecast> valuesHolder;

        public ValuesHolder()
        {
            valuesHolder = new ConcurrentDictionary<string, WeatherForecast>();
        }

        public void AddValue(WeatherForecast weatherForecast)
        {
            valuesHolder.TryAdd(weatherForecast.Date.ToString(), weatherForecast);
        }

        public WeatherForecast[] ToArray()
        {
            WeatherForecast[] weatherForecasts = new WeatherForecast[valuesHolder.Count];

            valuesHolder.Values.CopyTo(weatherForecasts, 0);

            return weatherForecasts;
        }

        public void UpdateValues(string oldValue, string newValue)
        {
            WeatherForecast weatherForecastNew = new WeatherForecast(newValue);
            WeatherForecast weatherForecastOld = new WeatherForecast(oldValue);

            valuesHolder.TryUpdate(weatherForecastOld.Date.ToString(), weatherForecastNew, weatherForecastOld);
        }

        public void DeleteValues(string valueToDelete)
        {
            WeatherForecast weatherForecastToDelete = new WeatherForecast(valueToDelete);
            valuesHolder.TryRemove(weatherForecastToDelete.Date.ToString(), out weatherForecastToDelete);


        }
    }

}
