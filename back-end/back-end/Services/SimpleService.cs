using back_end.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace back_end.Services
{
    public interface ISimpleService
    {
        Task<List<Currency>> GetCurrencies();
    }
    public class SimpleService: ISimpleService
    {

        public async Task<List<Currency>> GetCurrencies()
        {
            try
            {
                var client = new HttpClient();
                var url = "https://api.coinlore.net/api/tickers/";

                var response = await client.GetAsync(url);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseObj = new Dictionary<string, object>();
                    var dataStr = "";
                    var teste = null as List<Currency>;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());
                        dataStr = JsonConvert.SerializeObject(responseObj["data"]);
                        teste = JsonConvert.DeserializeObject<List<Currency>>(dataStr);
                    }


                    return teste;
                }

                return new List<Currency>();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        
    }
}
