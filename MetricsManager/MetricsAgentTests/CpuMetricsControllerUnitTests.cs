using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MetricsAgent;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        public CpuMetricsControllerUnitTests()
        {
            controller = new CpuMetricsController();
        }

        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange - подготовка данных
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act - выполнение действия
            var result = controller.GetMetricsFromAgent(fromTime, toTime);

            // Assert - проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
