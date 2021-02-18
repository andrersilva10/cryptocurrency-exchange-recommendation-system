using Models.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISimpleService
    {
        List<PairViewModel> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2);
        List<Currency> GetCurrencies();
    }
}
