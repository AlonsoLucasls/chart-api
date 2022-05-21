using ChartApi.Controllers.Models;

namespace ChartApi.Hubs.Interfaces
{
    public interface IChartHub
    {
        Task UpdateChart(List<BarChartModel> barChart);
    }
}
