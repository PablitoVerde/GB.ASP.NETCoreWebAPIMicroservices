using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.Data;
using MetricsAgent.DAL.Models;
using Core;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        // Инжектируем соединение с базой данных в наш репозиторий через конструктор
        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public void Update(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }

        public CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id=@id",
                    new { id = id });
            }
        }
    }
}
