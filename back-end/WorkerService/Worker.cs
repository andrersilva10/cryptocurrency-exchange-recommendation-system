using AppConfig;
using Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private AppConfiguration _appConfig = new AppConfiguration();
        private IExternalApiService _service;
        public Worker(ILogger<Worker> logger, IExternalApiService service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                
                _logger.LogInformation("Updating database at: {time}", DateTimeOffset.Now);
                try
                {
                    await _service.AddExchanges();
                    await _service.AddCurrencies();

                }catch(Exception e)
                {

                }
                _logger.LogInformation("Database updated at: {time}", DateTimeOffset.Now);
                await Task.Delay(_appConfig.Interval.Value, stoppingToken);
            }
        }
    }
}
