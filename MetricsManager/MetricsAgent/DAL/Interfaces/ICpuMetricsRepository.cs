using Core.Interfaces;

namespace MetricsAgent.DAL.Interfaces
{
    // Маркировочный интерфейс
    // используется, чтобы проверять работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
}
