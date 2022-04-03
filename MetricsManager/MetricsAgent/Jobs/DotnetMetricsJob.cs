using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;



namespace MetricsAgent.Jobs
{
    public class DotnetMetricsJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        private PerformanceCounter _DotnetCount;

        public DotnetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _DotnetCounter = new PerformanceCounter("Dotnet", "% Dotnet Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var DotnetUsageInPercents = Convert.ToInt32(_DotnetCounter.NextValue());

            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());


            _repository.Create(new DotNetMetric
            {
                Time = time,
                Value =
            DotnetUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}