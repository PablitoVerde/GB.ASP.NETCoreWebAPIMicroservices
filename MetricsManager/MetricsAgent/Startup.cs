using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;
using MetricsAgent.Repositories;
using MetricsAgent.DAL.Interfaces;
using Core.Interfaces;
using AutoMapper;
using FluentMigrator.Runner;
using System.IO;
using MetricsAgent.DAL;
using Quartz.Spi;
using MetricsAgent.Jobs;
using Quartz;
using Quartz.Impl;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string ConnectionString = "Data Source = metrics.db; Version = 3;";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

            services.AddTransient<INotifierMediatorService, NotifierMediatorService>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                    ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricsJob>();
            services.AddSingleton<RamMetricsJob>();
            services.AddSingleton<HddMetricsJob>();
            services.AddSingleton<DotnetMetricsJob>();
            services.AddSingleton<NetworkMetricsJob>();

            services.AddSingleton(new JobSchedule(
            jobType: typeof(CpuMetricsJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд

            services.AddSingleton(new JobSchedule(
            jobType: typeof(RamMetricsJob),
            cronExpression: "0/5 * * * * ?"));

            services.AddSingleton(new JobSchedule(
            jobType: typeof(HddMetricsJob),
            cronExpression: "0/5 * * * * ?"));

            services.AddSingleton(new JobSchedule(
            jobType: typeof(DotnetMetricsJob),
            cronExpression: "0/5 * * * * ?"));

            services.AddSingleton(new JobSchedule(
            jobType: typeof(NetworkMetricsJob),
            cronExpression: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
