using ChartApi.Controllers.Models;
using ChartApi.Hubs;
using ChartApi.Hubs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FactoryController : ControllerBase
    {

        private readonly ILogger<FactoryController> _logger;
        private readonly IHubContext<ChartHub, IChartHub> _chartHub;

        public FactoryController(ILogger<FactoryController> logger, IHubContext<ChartHub, IChartHub> chartHub)
        {
            _logger = logger;
            _chartHub = chartHub;
        }

        [HttpGet(Name = "FactoryData")]
        public IActionResult Get()
        {
            SetInterval(() => GetData(), TimeSpan.FromSeconds(5));

            return Ok("SignalR is working!");
        }
        private async Task SetInterval(Action action, TimeSpan timeout)
        {
            await Task.Delay(timeout).ConfigureAwait(false);

            action();

            await SetInterval(action, timeout);
        }
        private async Task GetData()
        {
            var barChartList = new List<BarChartModel>();
            for (var i = 0; i <= 3; i++)
            {
                var values = new List<int>();
                values.Add(getRandomIntIn(100, 4000));
                values.Add(getRandomIntIn(100, 4000));
                values.Add(getRandomIntIn(100, 4000));
                values.Add(getRandomIntIn(100, 4000));
                values.Add(getRandomIntIn(100, 4000));
                barChartList.Add(new BarChartModel { Mark = values });
            }

            await this._chartHub.Clients.All.UpdateChart(barChartList);
        }


        private int getRandomIntIn(int min, int max)
        {
            Random random = new Random();
            int numberGenerated = random.Next(min, max);

            return numberGenerated;
        }
    }
}