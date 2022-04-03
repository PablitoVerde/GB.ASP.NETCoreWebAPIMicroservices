using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;



namespace MetricsAgent.Jobs
{
    public class HddMetricsJob : IJob
    {
        private IHddMetricsRepository _repository;

        private PerformanceCounter _HddCount;

        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _HddCounter = new PerformanceCounter("Hdd", "% Hdd Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var HddUsageInPercents = Convert.ToInt32(_HddCounter.NextValue());

            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());


            _repository.Create(new HddMetric
            {
                Time = time,
                Value =
            HddUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}