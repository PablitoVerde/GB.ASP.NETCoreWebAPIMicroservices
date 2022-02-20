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
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ValuesHolder _holder;

        public WeatherForecastController(ValuesHolder _valuesHolder)
        {
            _holder = _valuesHolder;
        }


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
                _holder.AddValuesHolder(item);
            }

            return _holder.ToArray();
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {

            _holder.Add(input);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate,
        [FromQuery] string newValue)
        {           
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (holder.Values[i] == stringsToUpdate)
                    holder.Values[i] = newValue;
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            holder.Values = holder.Values.Where(w => w !=
            stringsToDelete).ToList();
            return Ok();
        }
}
