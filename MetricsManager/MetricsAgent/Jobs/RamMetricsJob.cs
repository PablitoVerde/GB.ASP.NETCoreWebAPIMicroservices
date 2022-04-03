using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricsJob : IJob
    {
        private IRamMetricsRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricsJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            ramCounter = new PerformanceCounter("Memory", "Available MByte");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());


            _repository.Create(new RamMetric
            {
                Time = time,
                Value =
            ramUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}