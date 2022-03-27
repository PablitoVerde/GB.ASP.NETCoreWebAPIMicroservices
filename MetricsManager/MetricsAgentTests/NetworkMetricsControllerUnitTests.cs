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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> mock2;
        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            mock2 = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(mock2.Object, mock.Object);
            
        }

        //Метод, выполняющий тест
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            var result = controller.Create(new MetricsAgent.Requests.NetworkMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}