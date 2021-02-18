using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IExchangeRepository
    {
        void AddExchanges(List<Exchange> exchanges);
        void AddCurrencies(List<Currency> currencies);
        List<Currency> GetCurrencies();
        List<Exchange> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2);
    }
}
