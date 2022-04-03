using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;



namespace MetricsAgent.Jobs
{
    public class CpuMetricsJob : IJob
    {
        private ICpuMetricsRepository _repository;

        private PerformanceCounter _cpuCount;

        public CpuMetricsJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
     
            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
   

            _repository.Create(new CpuMetric
            {
                Time = time,
                Value =
            cpuUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}