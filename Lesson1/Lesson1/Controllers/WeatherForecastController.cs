using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
a. Возможность сохранить температуру в указанное время.
b. Возможность отредактировать показатель температуры в указанное время.
c. Возможность удалить показатель температуры в указанный промежуток времени.
d. Возможность прочитать список показателей температуры за указанный промежуток
времени.
 */


namespace Lesson1.Controllers
{
    [ApiController]
    [Route("/")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ValuesHolder _holder;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ValuesHolder _valuesHolder)
        {
            _logger = logger;
            _holder = _valuesHolder;
        }

        //тестовый запрос
        [HttpPost("createTest")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            for (int i = 0; i < 5; i++)
            {
                var item = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]

                };
                _holder.AddValue(item);
            }

            return _holder.ToArray();
        }

        /// <summary>
        /// Запрос на создание записи о температуре в указанное время
        /// </summary>
        /// <param Дата="date"></param>
        /// <param Температура в цельсиях="temperatureC"></param>
        /// <param Описание="summary"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] WeatherForecast weatherForecast)
        {
           
            _holder.AddValue(weatherForecast);
            return Ok();
        }

        /// <summary>
        /// Запрос на получение имеющихся записей о погоде за определенное время
        /// </summary>
        /// <returns></returns>
        [HttpGet("read")]
        public IActionResult Read([FromBody] DateTime startDate, DateTime endDate)
        {
            return Ok(_holder.ToArray().Select(x => x.Date <= endDate && x.Date >= startDate).ToList());
        }

        /// <summary>
        /// Запрос на изменение значения температуры в указанный день
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string oldValue, [FromQuery] string newValue)
        {
            _holder.UpdateValues(oldValue, newValue);
            return Ok();
        }

        /// <summary>
        /// Запрос на удаление данных за определенный день
        /// </summary>
        /// <param name="valueToDelete"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string valueToDelete)
        {
            _holder.DeleteValues(valueToDelete);
            return Ok();
        }
    }
}
