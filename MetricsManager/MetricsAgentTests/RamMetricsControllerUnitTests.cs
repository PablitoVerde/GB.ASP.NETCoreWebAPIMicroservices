using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MetricsAgent.Controllers;
using Moq;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using MetricsAgent;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> mock2;
        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            mock2 = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mock2.Object, mock.Object);
        }

        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = controller.Create(new MetricsAgent.Requests.RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}