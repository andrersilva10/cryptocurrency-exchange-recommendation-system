using AppConfig;
using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class SimpleService : ISimpleService
    {

        private HttpClient _httpClient;
        private readonly IConfiguration _config;
        private AppConfiguration _appConfiguration;
        public SimpleService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _appConfiguration = new AppConfiguration();
            _config = config;

        }
        public async Task<List<Currency>> GetCurrencies()
        {
            try
            {
                //var url = _config.GetValue<string>("Apis:CoinLore:GetCurrencies");
                var url = _appConfiguration.GetCurrencies;
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseObj = new Dictionary<string, object>();
                    var dataStr = "";
                    var deserializedCurrencies = null as List<Currency>;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());
                        dataStr = JsonConvert.SerializeObject(responseObj["data"]);

                        deserializedCurrencies = JsonConvert.DeserializeObject<List<Currency>>(dataStr);
                    }
                    return deserializedCurrencies;
                }
                return new List<Currency>();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<List<Exchange>> GetExchanges()
        {
            try
            {
                //var url = _config.GetValue<string>("Apis:CoinLore:GetExchanges");
                var url = _appConfiguration.GetExchanges;

                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseObj = new Dictionary<string, object>();
                    var dataStr = "";
                    var deserializedCurrencies = new List<Exchange>();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());
                        var exchanges = new List<Exchange>();
                        foreach (var key in responseObj.Keys)
                        {
                            dataStr = JsonConvert.SerializeObject(responseObj[key]);
                            var obj = JsonConvert.DeserializeObject<Exchange>(dataStr);
                            deserializedCurrencies.Add(obj);
                        }

                    }
                    return deserializedCurrencies;
                }
                return new List<Exchange>();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<List<ExchangePair>> GetPairsByExchangeId(int idExchange)
        {
            try
            {
                //var url = _config.GetValue<string>("Apis:CoinLore:GetPairsFromOneExchange") + $"?id={idExchange}";

                var url = _appConfiguration.GetPairsFromOneExchange + $"?id={idExchange}";

                var response = await _httpClient.GetAsync(url);
                var responseObj = new Dictionary<string, object>();
                var dataStr = "";
                var deserializedPairs = new List<ExchangePair>();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());

                        dataStr = JsonConvert.SerializeObject(responseObj["pairs"]);

                        deserializedPairs = JsonConvert.DeserializeObject<List<ExchangePair>>(dataStr);

                    }
                }
                return deserializedPairs;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Exchange>> GetExchangeWithPairs()
        {
            var exchanges = await GetExchanges();
            foreach (var item in exchanges)
            {
                item.Pairs = await GetPairsByExchangeId(item.Id);
                item.Pairs.ForEach(x => x.IdExchange = item.Id);
            }
            return exchanges;
        }

    }
}
