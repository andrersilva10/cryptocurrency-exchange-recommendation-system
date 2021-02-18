using Domain.Interfaces;
using Domain.Services;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<IExternalApiService, CoinLoreService>();
                    services.AddTransient<ExchangeContext, ExchangeContext>();
                    services.AddTransient<IExchangeRepository, ExchangeRepository>();
                });
    }
}
