using ChartApi.Controllers.Models;
using ChartApi.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChartApi.Hubs
{
    public class ChartHub : Hub<IChartHub>
    {
        public async Task UpdateChart(List<BarChartModel> barChartModel)
        {
            await Clients.Caller.UpdateChart(barChartModel);
        }
    }
}
