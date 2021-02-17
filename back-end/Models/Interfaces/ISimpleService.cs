using Models.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISimpleService
    {
        Task<List<Currency>> GetCurrencies();
        Task<List<Exchange>> GetExchanges();
        Task<List<ExchangePair>> GetPairsByExchangeId(int idExchange);
        Task<List<Exchange>> GetExchangeWithPairs();
        Task AddExchanges();
        List<PairViewModel> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2);
    }
}
