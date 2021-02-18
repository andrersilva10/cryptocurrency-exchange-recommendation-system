using Domain.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Infrastructure
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly ExchangeContext _context;

        public ExchangeRepository(ExchangeContext context)
        {
            _context = context;
        }

   
        public void AddExchanges(List<Exchange> exchanges)
        {
            clearExchanges();
            _context.AddRange(exchanges);
            _context.SaveChanges();
        }

        public void AddCurrencies(List<Currency> currencies)
        {
            clearCurrencies();
            _context.AddRange(currencies);

            _context.SaveChanges();
        }

        public List<Currency> GetCurrencies()
        {
            var currencies = _context.Currencies.ToList();
            return currencies;
        }
        public List<Exchange> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2)
        {
            
            var exchanges = _context.ExchangePairs.Where(
                    x => x.Price > 0M && x.Base == currencySymbol1 
                    && (currencySymbol2 != "USD" ? x.Quote == currencySymbol2 : true)
                )
                .Select(x => x.Exchange).Distinct().ToList();


            exchanges.ForEach(x => {
                x.ExchangePairs = _context.ExchangePairs.Where(y => y.IdExchange == x.Id && y.Base == currencySymbol1
                && (currencySymbol2 != "USD" ? y.Quote == currencySymbol2 : true)).ToList();
                x.ExchangePairs = x.ExchangePairs.Where(y => y.Time == x.ExchangePairs.Max(z => z.Time)).ToList();
            });
            return exchanges;
        }

        public void clearExchanges()
        {
            var allRows = _context.Exchanges.ToList();
            _context.Exchanges.RemoveRange(allRows);
            _context.SaveChanges();
        }

        public void clearCurrencies()
        {
            
            var allRows = _context.Currencies.ToList();
            _context.Currencies.RemoveRange(allRows);
            _context.SaveChanges();
        }
    }
}
