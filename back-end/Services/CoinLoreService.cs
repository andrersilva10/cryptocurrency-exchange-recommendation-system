using AppConfig;
using Domain.Interfaces;
using Models.Models;
using Domain.Services;
using Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
namespace Services
{
    public class CoinLoreService : IExternalApiService
    {

        private HttpClient _httpClient;
        private AppConfiguration _appConfiguration;
        private IExchangeRepository _repository;
        public CoinLoreService(IExchangeRepository repository)
        {
            _httpClient = new HttpClient();
            _appConfiguration = new AppConfiguration();
            _repository = repository;
        }
        public async Task<List<Currency>> GetCurrencies()
        {
            try
            {
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
            catch (Exception)
            {
                throw new Exception("error connecting to the CoinLore API");
            }
        }

        public async Task<List<Exchange>> GetExchanges()
        {
            try
            {
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
            catch (Exception)
            {
                throw new Exception("error connecting to the CoinLore API");
            }
        }

        public async Task<List<ExchangePair>> GetPairsByExchangeId(int idExchange)
        {
            try
            {
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

                        if(responseObj != null)
                        {
                            dataStr = JsonConvert.SerializeObject(responseObj["pairs"]);
                            deserializedPairs = JsonConvert.DeserializeObject<List<ExchangePair>>(dataStr);
                        }


                    }
                }
                return deserializedPairs;

            }
            catch (Exception)
            {
                throw new Exception("error connecting to the CoinLore API");
            }
        }
        public async Task<List<Exchange>> GetExchangeWithPairs()
        {
            var exchanges = await GetExchanges();
            int i = 0;
            foreach (var item in exchanges)
            {
                item.ExchangePairs = await GetPairsByExchangeId(item.Id);
                item.ExchangePairs.ForEach(x => x.IdExchange = item.Id);
                i++;
                //if (i > 10) break;
            }
            return exchanges;
        }

        public async Task AddExchanges()
        {
            var exchanges = await GetExchangeWithPairs();
            _repository.AddExchanges(exchanges);

        }

        public async Task AddCurrencies()
        {
            var currencies = await GetCurrencies();
            currencies.Add(new Currency() { Id = currencies.Max(x => x.Id) + 1, Name = "USD", NameId = "us_dollar", Symbol = "USD" });
            _repository.AddCurrencies(currencies);

        }


    }
}
