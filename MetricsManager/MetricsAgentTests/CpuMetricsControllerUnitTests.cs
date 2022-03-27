using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> mock2;
        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mock2 = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mock2.Object, mock.Object);
        }

        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
    }
}
