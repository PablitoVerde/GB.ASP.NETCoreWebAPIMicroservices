using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;



namespace MetricsAgent.Jobs
{
    public class NetworkMetricsJob : IJob
    {
        private INetworkMetricsRepository _repository;

        private PerformanceCounter _NetworkCount;

        public NetworkMetricsJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _NetworkCounter = new PerformanceCounter("Network", "% Network Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var NetworkUsageInPercents = Convert.ToInt32(_NetworkCounter.NextValue());

            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());


            _repository.Create(new NetworkMetric
            {
                Time = time,
                Value =
            NetworkUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}