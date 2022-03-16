using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        public NetworkMetricsControllerUnitTests()
        {
            controller = new NetworkMetricsController();
        }
        
        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange - подготовка данных
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            
            //Act - выполнение действия
            var result = controller.GetMetricsFromAgent(agentId, fromTime,
            toTime);
            
            // Assert - проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
