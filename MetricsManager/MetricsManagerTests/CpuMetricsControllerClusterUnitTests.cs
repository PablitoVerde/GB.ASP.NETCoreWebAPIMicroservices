using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerClusterUnitTests
    {
        private CpuMetricsController controller;
        public CpuMetricsControllerClusterUnitTests()
        {
            controller = new CpuMetricsController();
        }
        
        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            //Arrange - подготовка данных
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            
            //Act - выполнение действия
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            
            // Assert - проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
