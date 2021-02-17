using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AppConfig
{
    public class AppConfiguration
    {
        public readonly string _connectionString = string.Empty;
        public int? Interval { get; set; } = 5000;
        public string GetCurrencies { get; set; }
        public string GetExchanges { get; set; }
        public string GetPairsFromOneExchange { get; set; }

        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            GetCurrencies = root.GetSection("Apis").GetSection("CoinLore").GetSection("GetCurrencies").Value;
            GetExchanges = root.GetSection("Apis").GetSection("CoinLore").GetSection("GetExchanges").Value;
            GetPairsFromOneExchange = root.GetSection("Apis").GetSection("CoinLore").GetSection("GetPairsFromOneExchange").Value;

            if (root.GetSection("ServiceConfigurations").GetSection("Interval").Value != null)
            {
                Interval = Convert.ToInt32(root.GetSection("ServiceConfigurations").GetSection("Interval").Value);
            }

            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            var appSetting = root.GetSection("ApplicationSettings");
        }
        public string ConnectionString
        {
            get => _connectionString;
        }
    }
}
