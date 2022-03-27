
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsController>> mock2;
        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mock2 = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mock2.Object, mock.Object);
        }

        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            var result = controller.Create(new MetricsAgent.Requests.DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
}