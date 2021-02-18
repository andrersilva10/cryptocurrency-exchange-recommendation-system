using System.Collections.Generic;
using Domain.Interfaces;
using Models.Interfaces;
using Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Models.Models;

namespace Services
{
    public class SimpleService : ISimpleService
    {
        private IExchangeRepository _repository;
        public SimpleService(IExchangeRepository repository)
        {
            _repository = repository;
        }

        public List<Currency> GetCurrencies()
        {
            var currencies = _repository.GetCurrencies();
            return currencies;
        }


        public List<PairViewModel> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2)
        {
            var pairs = _repository.GetExchangesByTwoCurrencies(currencySymbol1, currencySymbol2).Select(x => new PairViewModel(x)).OrderBy(x => x.PairPrice).ToList();

            pairs.Where(x => x.PairPrice == pairs.Min(y => y.PairPrice)).ToList().ForEach(x => x.BestRate = true);

            return pairs;
        }
    }
}
