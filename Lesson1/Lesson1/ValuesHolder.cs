using System.Collections;
using System.Collections.Generic;

namespace Lesson1
{
    /// <summary>
    /// Класс создания списка значений объектов класса WeatherForecast
    /// </summary>
    public class ValuesHolder
    {
        public List<WeatherForecast> valuesHolder;
        public ValuesHolder()
        {
            valuesHolder = new List<WeatherForecast>();

        }

        public void AddValuesHolder(WeatherForecast value)
        {
            valuesHolder.Add(value);
        }

        public WeatherForecast[] ToArray()
        {
            return valuesHolder.ToArray();
        }

        public IEnumerator GetEnumerator()
        {
            int i = 0;
            yield return i++;
        }
}
